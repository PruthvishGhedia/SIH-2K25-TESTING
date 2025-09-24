using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IHostelService
{
    [OperationContract]
    Task<IEnumerable<Hostel>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Hostel?> GetAsync(int hostel_id);

    [OperationContract]
    Task<Hostel> CreateAsync(Hostel item);

    [OperationContract]
    Task<Hostel?> UpdateAsync(int hostel_id, Hostel item);

    [OperationContract]
    Task<Hostel?> RemoveAsync(int hostel_id);
}