using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class UserService : IUserService
{
    private readonly UserRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public UserService(UserRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<User> CreateAsync(User item)
    {
        var user = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveUserUpdate", user);
        
        return user;
    }
    
    public async Task<User?> GetAsync(string user_id)
    {
        if (int.TryParse(user_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<User>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<User?> RemoveAsync(string user_id)
    {
        if (int.TryParse(user_id, out int id))
        {
            var user = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (user != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUserUpdate", user);
            }
            
            return user;
        }
        return null;
    }
    
    public async Task<User?> UpdateAsync(string user_id, User item)
    {
        if (int.TryParse(user_id, out int id))
        {
            var user = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (user != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveUserUpdate", user);
            }
            
            return user;
        }
        return null;
    }
}