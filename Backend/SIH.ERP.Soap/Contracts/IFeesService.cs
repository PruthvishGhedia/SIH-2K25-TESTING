using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IFeesService
{
    [OperationContract]
    Task<IEnumerable<Fees>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Fees?> GetAsync(int fee_id);

    [OperationContract]
    Task<Fees> CreateAsync(Fees item);

    [OperationContract]
    Task<Fees?> UpdateAsync(int fee_id, Fees item);

    [OperationContract]
    Task<Fees?> RemoveAsync(int fee_id);
}