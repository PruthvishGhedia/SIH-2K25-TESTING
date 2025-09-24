using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IPaymentService
{
    [OperationContract]
    Task<IEnumerable<Payment>> ListAsync(int limit = 100, int offset = 0);
    [OperationContract]
    Task<Payment?> GetAsync(int payment_id);
    [OperationContract]
    Task<Payment> CreateAsync(Payment item);
    [OperationContract]
    Task<Payment?> UpdateAsync(int payment_id, Payment item);
    [OperationContract]
    Task<Payment?> RemoveAsync(int payment_id);
}

