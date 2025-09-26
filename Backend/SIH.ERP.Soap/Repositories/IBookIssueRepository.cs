using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IBookIssueRepository : IRepository
{
    Task<IEnumerable<BookIssue>> ListAsync(int limit, int offset);
    Task<BookIssue?> GetAsync(int id);
    Task<BookIssue> CreateAsync(BookIssue item);
    Task<BookIssue?> UpdateAsync(int id, BookIssue item);
    Task<BookIssue?> RemoveAsync(int id);
}