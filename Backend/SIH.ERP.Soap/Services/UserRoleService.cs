using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class UserRoleService : IUserRoleService
{
    private readonly UserRoleRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public UserRoleService(UserRoleRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<UserRole> CreateAsync(UserRole item)
    {
        var userRole = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveUserRoleUpdate", userRole);
        
        return userRole;
    }
    
    public async Task<UserRole?> GetAsync(string user_role_id)
    {
        if (int.TryParse(user_role_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<UserRole>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<UserRole?> RemoveAsync(string user_role_id)
    {
        if (int.TryParse(user_role_id, out int id))
        {
            var userRole = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (userRole != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUserRoleUpdate", userRole);
            }
            
            return userRole;
        }
        return null;
    }
    
    public async Task<UserRole?> UpdateAsync(string user_role_id, UserRole item)
    {
        if (int.TryParse(user_role_id, out int id))
        {
            var userRole = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (userRole != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUserRoleUpdate", userRole);
            }
            
            return userRole;
        }
        return null;
    }
}