using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IAdmissionService
{
    [OperationContract]
    Task<IEnumerable<Admission>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Admission?> GetAsync(int admission_id);

    [OperationContract]
    Task<Admission> CreateAsync(Admission item);

    [OperationContract]
    Task<Admission?> UpdateAsync(int admission_id, Admission item);

    [OperationContract]
    Task<Admission?> RemoveAsync(int admission_id);
}