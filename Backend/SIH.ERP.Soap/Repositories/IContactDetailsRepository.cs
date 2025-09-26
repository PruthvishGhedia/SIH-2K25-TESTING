using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IContactDetailsRepository : IRepository
{
    Task<IEnumerable<ContactDetails>> ListAsync(int limit, int offset);
    Task<ContactDetails?> GetAsync(int id);
    Task<ContactDetails> CreateAsync(ContactDetails item);
    Task<ContactDetails?> UpdateAsync(int id, ContactDetails item);
    Task<ContactDetails?> RemoveAsync(int id);
}