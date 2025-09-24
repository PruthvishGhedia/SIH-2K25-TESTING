using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class BookIssueService : IBookIssueService
{
    private readonly BookIssueRepository _repo;
    public BookIssueService(BookIssueRepository repo)
    {
        _repo = repo;
    }

    public Task<BookIssue> CreateAsync(BookIssue item) => _repo.CreateAsync(item);
    public Task<BookIssue?> GetAsync(int issue_id) => _repo.GetAsync(issue_id);
    public Task<IEnumerable<BookIssue>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<BookIssue?> RemoveAsync(int issue_id) => _repo.RemoveAsync(issue_id);
    public Task<BookIssue?> UpdateAsync(int issue_id, BookIssue item) => _repo.UpdateAsync(issue_id, item);
}