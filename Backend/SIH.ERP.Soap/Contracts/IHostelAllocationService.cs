using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IHostelAllocationService
{
    [OperationContract]
    Task<IEnumerable<HostelAllocation>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<HostelAllocation?> GetAsync(int allocation_id);

    [OperationContract]
    Task<HostelAllocation> CreateAsync(HostelAllocation item);

    [OperationContract]
    Task<HostelAllocation?> UpdateAsync(int allocation_id, HostelAllocation item);

    [OperationContract]
    Task<HostelAllocation?> RemoveAsync(int allocation_id);
}