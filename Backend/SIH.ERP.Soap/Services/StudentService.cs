using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class StudentService : IStudentService
{
    private readonly StudentRepository _repo;
    public StudentService(StudentRepository repo) { _repo = repo; }

    public Task<Student> CreateAsync(Student item) => _repo.CreateAsync(item);
    public Task<Student?> GetAsync(int student_id) => _repo.GetAsync(student_id);
    public Task<IEnumerable<Student>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Student?> RemoveAsync(int student_id) => _repo.RemoveAsync(student_id);
    public Task<Student?> UpdateAsync(int student_id, Student item) => _repo.UpdateAsync(student_id, item);
}

