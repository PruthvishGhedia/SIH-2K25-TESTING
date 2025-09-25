using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public EnrollmentService(IEnrollmentRepository repo, IHubContext<DashboardHub> hubContext) 
    { 
        _repo = repo; 
        _hubContext = hubContext;
    }

    public Task<IEnumerable<Enrollment>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Enrollment?> GetAsync(string enrollment_id)
    {
        if (int.TryParse(enrollment_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }

    public async Task<Enrollment> CreateAsync(Enrollment item)
    {
        Validate(item);
        var enrollment = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveEnrollmentUpdate", enrollment);
        
        return enrollment;
    }

    public async Task<Enrollment?> UpdateAsync(string enrollment_id, Enrollment item)
    {
        if (int.TryParse(enrollment_id, out int id))
        {
            Validate(item);
            var enrollment = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (enrollment != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveEnrollmentUpdate", enrollment);
            }
            
            return enrollment;
        }
        return null;
    }

    public async Task<Enrollment?> RemoveAsync(string enrollment_id)
    {
        if (int.TryParse(enrollment_id, out int id))
        {
            var enrollment = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (enrollment != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveEnrollmentUpdate", enrollment);
            }
            
            return enrollment;
        }
        return null;
    }

    private void Validate(Enrollment e)
    {
        if (e.student_id <= 0) throw new FaultException("student_id is required");
        if (e.course_id <= 0) throw new FaultException("course_id is required");
        if (string.IsNullOrWhiteSpace(e.enrollment_date)) throw new FaultException("enrollment_date is required");
    }
}