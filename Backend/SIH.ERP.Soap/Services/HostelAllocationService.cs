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

    public Task<HostelAllocation> CreateAsync(HostelAllocation item) => _repo.CreateAsync(item);
    public Task<HostelAllocation?> GetAsync(int allocation_id) => _repo.GetAsync(allocation_id);
    public Task<IEnumerable<HostelAllocation>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<HostelAllocation?> RemoveAsync(int allocation_id) => _repo.RemoveAsync(allocation_id);
    public Task<HostelAllocation?> UpdateAsync(int allocation_id, HostelAllocation item) => _repo.UpdateAsync(allocation_id, item);
}