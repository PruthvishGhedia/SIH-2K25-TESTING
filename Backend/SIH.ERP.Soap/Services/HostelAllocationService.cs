using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class HostelAllocationService : IHostelAllocationService
{
    private readonly HostelAllocationRepository _repo;
    public HostelAllocationService(HostelAllocationRepository repo)
    {
        _repo = repo;
    }

    public async Task<HostelAllocation> CreateAsync(HostelAllocation item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<HostelAllocation?> GetAsync(string allocation_id)
    {
        if (int.TryParse(allocation_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<HostelAllocation>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<HostelAllocation?> RemoveAsync(string allocation_id)
    {
        if (int.TryParse(allocation_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<HostelAllocation?> UpdateAsync(string allocation_id, HostelAllocation item)
    {
        if (int.TryParse(allocation_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}