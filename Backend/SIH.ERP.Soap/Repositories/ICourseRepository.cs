using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface ICourseRepository : IRepository
{
    Task<IEnumerable<Course>> ListAsync(int limit, int offset);
    Task<Course?> GetAsync(int id);
    Task<Course> CreateAsync(Course item);
    Task<Course?> UpdateAsync(int id, Course item);
    Task<Course?> RemoveAsync(int id);
}