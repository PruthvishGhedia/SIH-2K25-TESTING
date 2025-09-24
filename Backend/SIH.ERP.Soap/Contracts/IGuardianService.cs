using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IGuardianService
{
    [OperationContract]
    Task<IEnumerable<Guardian>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Guardian?> GetAsync(int guardian_id);

    [OperationContract]
    Task<Guardian> CreateAsync(Guardian item);

    [OperationContract]
    Task<Guardian?> UpdateAsync(int guardian_id, Guardian item);

    [OperationContract]
    Task<Guardian?> RemoveAsync(int guardian_id);
}