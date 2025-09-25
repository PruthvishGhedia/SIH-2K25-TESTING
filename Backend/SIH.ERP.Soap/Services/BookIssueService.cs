using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class BookIssueService : IBookIssueService
{
    private readonly BookIssueRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public BookIssueService(BookIssueRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public Task<BookIssue> CreateAsync(BookIssue item) => _repo.CreateAsync(item);
    
    public async Task<BookIssue?> GetAsync(string issue_id)
    {
        if (int.TryParse(issue_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<BookIssue>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<BookIssue?> RemoveAsync(string issue_id)
    {
        if (int.TryParse(issue_id, out int id))
        {
            var bookIssue = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (bookIssue != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveBookIssueUpdate", bookIssue);
            }
            
            return bookIssue;
        }
        return null;
    }
    
    public async Task<BookIssue?> UpdateAsync(string issue_id, BookIssue item)
    {
        if (int.TryParse(issue_id, out int id))
        {
            var bookIssue = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (bookIssue != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveBookIssueUpdate", bookIssue);
            }
            
            return bookIssue;
        }
        return null;
    }
}