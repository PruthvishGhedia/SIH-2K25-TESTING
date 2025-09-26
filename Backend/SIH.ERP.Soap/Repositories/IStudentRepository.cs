using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IStudentRepository : IRepository
{
    Task<IEnumerable<Student>> ListAsync(int limit, int offset);
    Task<Student?> GetAsync(int id);
    Task<Student> CreateAsync(Student item);
    Task<Student?> UpdateAsync(int id, Student item);
    Task<Student?> RemoveAsync(int id);
}