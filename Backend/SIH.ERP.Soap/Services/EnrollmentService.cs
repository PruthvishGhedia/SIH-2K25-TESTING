using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo;
    public EnrollmentService(IEnrollmentRepository repo) { _repo = repo; }

    public Task<IEnumerable<Enrollment>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Enrollment?> GetAsync(int enrollment_id) => _repo.GetAsync(enrollment_id);

    public async Task<Enrollment> CreateAsync(Enrollment item)
    {
        Validate(item);
        return await _repo.CreateAsync(item);
    }

    public async Task<Enrollment?> UpdateAsync(int enrollment_id, Enrollment item)
    {
        Validate(item);
        return await _repo.UpdateAsync(enrollment_id, item);
    }

    public Task<Enrollment?> RemoveAsync(int enrollment_id) => _repo.RemoveAsync(enrollment_id);

    private void Validate(Enrollment e)
    {
        if (e.student_id <= 0) throw new FaultException("student_id is required");
        if (e.course_id <= 0) throw new FaultException("course_id is required");
        if (string.IsNullOrWhiteSpace(e.enrollment_date)) throw new FaultException("enrollment_date is required");
    }
}

