using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class FacultyService : IFacultyService
{
    private readonly IFacultyRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public FacultyService(IFacultyRepository repo, IHubContext<DashboardHub> hubContext) 
    { 
        _repo = repo; 
        _hubContext = hubContext;
    }

    public Task<IEnumerable<Faculty>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Faculty?> GetAsync(string faculty_id)
    {
        if (int.TryParse(faculty_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }

    public async Task<Faculty> CreateAsync(Faculty item)
    {
        Validate(item);
        var faculty = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveFacultyUpdate", faculty);
        
        return faculty;
    }

    public async Task<Faculty?> UpdateAsync(string faculty_id, Faculty item)
    {
        if (int.TryParse(faculty_id, out int id))
        {
            Validate(item);
            var faculty = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (faculty != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveFacultyUpdate", faculty);
            }
            
            return faculty;
        }
        return null;
    }

    public async Task<Faculty?> RemoveAsync(string faculty_id)
    {
        if (int.TryParse(faculty_id, out int id))
        {
            var faculty = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (faculty != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveFacultyUpdate", faculty);
            }
            
            return faculty;
        }
        return null;
    }

    private void Validate(Faculty f)
    {
        if (string.IsNullOrWhiteSpace(f.first_name)) throw new FaultException("first_name is required");
        if (string.IsNullOrWhiteSpace(f.last_name)) throw new FaultException("last_name is required");
        if (string.IsNullOrWhiteSpace(f.email)) throw new FaultException("email is required");
    }
}