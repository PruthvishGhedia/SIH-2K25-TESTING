using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IExamRepository : IRepository
{
    Task<IEnumerable<Exam>> ListAsync(int limit, int offset);
    Task<Exam?> GetAsync(int id);
    Task<Exam> CreateAsync(Exam item);
    Task<Exam?> UpdateAsync(int id, Exam item);
    Task<Exam?> RemoveAsync(int id);
}