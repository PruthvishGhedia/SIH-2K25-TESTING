using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IHostelAllocationRepository : IRepository
{
    Task<IEnumerable<HostelAllocation>> ListAsync(int limit, int offset);
    Task<HostelAllocation?> GetAsync(int id);
    Task<HostelAllocation> CreateAsync(HostelAllocation item);
    Task<HostelAllocation?> UpdateAsync(int id, HostelAllocation item);
    Task<HostelAllocation?> RemoveAsync(int id);
}