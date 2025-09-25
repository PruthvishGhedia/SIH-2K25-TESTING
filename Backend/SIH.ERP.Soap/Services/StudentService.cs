using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public StudentService(IStudentRepository repo, IHubContext<DashboardHub> hubContext) 
    { 
        _repo = repo; 
        _hubContext = hubContext;
    }

    public Task<IEnumerable<Student>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);

    public async Task<Student?> GetAsync(string student_id)
    {
        if (int.TryParse(student_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }

    public async Task<Student> CreateAsync(Student item)
    {
        ValidateStudent(item);
        var student = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveStudentUpdate", student);
        
        return student;
    }

    public async Task<Student?> UpdateAsync(string student_id, Student item)
    {
        if (int.TryParse(student_id, out int id))
        {
            ValidateStudent(item);
            var student = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (student != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveStudentUpdate", student);
            }
            
            return student;
        }
        return null;
    }

    public async Task<Student?> RemoveAsync(string student_id)
    {
        if (int.TryParse(student_id, out int id))
        {
            var student = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (student != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveStudentUpdate", student);
            }
            
            return student;
        }
        return null;
    }

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

