using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IContactDetailsService
{
    [OperationContract]
    Task<IEnumerable<ContactDetails>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<ContactDetails?> GetAsync(int contact_id);

    [OperationContract]
    Task<ContactDetails> CreateAsync(ContactDetails item);

    [OperationContract]
    Task<ContactDetails?> UpdateAsync(int contact_id, ContactDetails item);

    [OperationContract]
    Task<ContactDetails?> RemoveAsync(int contact_id);
}