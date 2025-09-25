using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class ResultService : IResultService
{
    private readonly ResultRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public ResultService(ResultRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<Result> CreateAsync(Result item)
    {
        var result = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveResultUpdate", result);
        
        return result;
    }
    
    public async Task<Result?> GetAsync(string result_id)
    {
        if (int.TryParse(result_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Result>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Result?> RemoveAsync(string result_id)
    {
        if (int.TryParse(result_id, out int id))
        {
            var result = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (result != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveResultUpdate", result);
            }
            
            return result;
        }
        return null;
    }
    
    public async Task<Result?> UpdateAsync(string result_id, Result item)
    {
        if (int.TryParse(result_id, out int id))
        {
            var result = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (result != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveResultUpdate", result);
            }
            
            return result;
        }
        return null;
    }
}