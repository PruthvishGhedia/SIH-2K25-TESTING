using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repo;
    public AttendanceService(IAttendanceRepository repo) { _repo = repo; }

    public Task<IEnumerable<Attendance>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Attendance?> GetAsync(int attendance_id) => _repo.GetAsync(attendance_id);

    public async Task<Attendance> CreateAsync(Attendance item)
    {
        Validate(item);
        return await _repo.CreateAsync(item);
    }

    public async Task<Attendance?> UpdateAsync(int attendance_id, Attendance item)
    {
        Validate(item);
        return await _repo.UpdateAsync(attendance_id, item);
    }

    public Task<Attendance?> RemoveAsync(int attendance_id) => _repo.RemoveAsync(attendance_id);

    private void Validate(Attendance a)
    {
        if (a.student_id <= 0) throw new FaultException("student_id is required");
        if (a.course_id <= 0) throw new FaultException("course_id is required");
        if (string.IsNullOrWhiteSpace(a.date)) throw new FaultException("date is required");
    }
}

