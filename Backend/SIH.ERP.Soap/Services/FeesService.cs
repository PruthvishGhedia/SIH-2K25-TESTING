using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class FeesService : IFeesService
{
    private readonly FeesRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public FeesService(FeesRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<Fees> CreateAsync(Fees item)
    {
        var fees = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveFeesUpdate", fees);
        
        return fees;
    }
    
    public async Task<Fees?> GetAsync(string fee_id)
    {
        if (int.TryParse(fee_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Fees>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Fees?> RemoveAsync(string fee_id)
    {
        if (int.TryParse(fee_id, out int id))
        {
            var fees = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (fees != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveFeesUpdate", fees);
            }
            
            return fees;
        }
        return null;
    }
    
    public async Task<Fees?> UpdateAsync(string fee_id, Fees item)
    {
        if (int.TryParse(fee_id, out int id))
        {
            var fees = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (fees != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveFeesUpdate", fees);
            }
            
            return fees;
        }
        return null;
    }
}