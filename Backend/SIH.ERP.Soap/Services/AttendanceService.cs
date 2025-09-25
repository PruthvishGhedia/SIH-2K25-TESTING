using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public AttendanceService(IAttendanceRepository repo, IHubContext<DashboardHub> hubContext) 
    { 
        _repo = repo; 
        _hubContext = hubContext;
    }

    public Task<IEnumerable<Attendance>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Attendance?> GetAsync(string attendance_id)
    {
        if (int.TryParse(attendance_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }

    public async Task<Attendance> CreateAsync(Attendance item)
    {
        Validate(item);
        var attendance = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveAttendanceUpdate", attendance);
        
        return attendance;
    }

    public async Task<Attendance?> UpdateAsync(string attendance_id, Attendance item)
    {
        if (int.TryParse(attendance_id, out int id))
        {
            Validate(item);
            var attendance = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (attendance != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveAttendanceUpdate", attendance);
            }
            
            return attendance;
        }
        return null;
    }

    public async Task<Attendance?> RemoveAsync(string attendance_id)
    {
        if (int.TryParse(attendance_id, out int id))
        {
            var attendance = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (attendance != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveAttendanceUpdate", attendance);
            }
            
            return attendance;
        }
        return null;
    }

    private void Validate(Attendance a)
    {
        if (a.student_id <= 0) throw new FaultException("student_id is required");
        if (a.course_id <= 0) throw new FaultException("course_id is required");
        if (string.IsNullOrWhiteSpace(a.date)) throw new FaultException("date is required");
    }
}