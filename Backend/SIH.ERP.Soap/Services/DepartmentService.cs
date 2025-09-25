using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class DepartmentService : IDepartmentService
{
    private readonly DepartmentRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public DepartmentService(DepartmentRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<Department> CreateAsync(Department item)
    {
        var department = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveDepartmentUpdate", department);
        
        return department;
    }
    
    public async Task<Department?> GetAsync(string dept_id)
    {
        if (int.TryParse(dept_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Department>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Department?> RemoveAsync(string dept_id)
    {
        if (int.TryParse(dept_id, out int id))
        {
            var department = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (department != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveDepartmentUpdate", department);
            }
            
            return department;
        }
        return null;
    }
    
    public async Task<Department?> UpdateAsync(string dept_id, Department item)
    {
        if (int.TryParse(dept_id, out int id))
        {
            var department = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (department != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveDepartmentUpdate", department);
            }
            
            return department;
        }
        return null;
    }
}