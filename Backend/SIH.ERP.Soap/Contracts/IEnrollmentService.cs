using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IEnrollmentService
{
    [OperationContract]
    Task<IEnumerable<Enrollment>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Enrollment?> GetAsync(int enrollment_id);
    [OperationContract]
    Task<Enrollment> CreateAsync(Enrollment item);
    [OperationContract]
    Task<Enrollment?> UpdateAsync(int enrollment_id, Enrollment item);
    [OperationContract]
    Task<Enrollment?> RemoveAsync(int enrollment_id);
}

