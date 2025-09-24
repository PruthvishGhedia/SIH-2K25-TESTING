using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    
    public StudentService(IStudentRepository repo) 
    { 
        _repo = repo; 
    }

    public Task<IEnumerable<Student>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);

    public Task<Student?> GetAsync(int student_id) => _repo.GetAsync(student_id);

    public async Task<Student> CreateAsync(Student item)
    {
        ValidateStudent(item);
        return await _repo.CreateAsync(item);
    }

    public async Task<Student?> UpdateAsync(int student_id, Student item)
    {
        ValidateStudent(item);
        return await _repo.UpdateAsync(student_id, item);
    }

    public Task<Student?> RemoveAsync(int student_id) => _repo.RemoveAsync(student_id);

    private void ValidateStudent(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.first_name))
            throw new FaultException("first_name is required");
            
        if (string.IsNullOrWhiteSpace(student.last_name))
            throw new FaultException("last_name is required");
            
        if (string.IsNullOrWhiteSpace(student.email))
            throw new FaultException("email is required");
            
        if (!IsValidEmail(student.email))
            throw new FaultException("email format is invalid");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

