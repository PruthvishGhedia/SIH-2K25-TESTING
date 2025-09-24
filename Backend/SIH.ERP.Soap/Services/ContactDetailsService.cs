using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class ContactDetailsService : IContactDetailsService
{
    private readonly ContactDetailsRepository _repo;
    public ContactDetailsService(ContactDetailsRepository repo)
    {
        _repo = repo;
    }

    public Task<ContactDetails> CreateAsync(ContactDetails item) => _repo.CreateAsync(item);
    public Task<ContactDetails?> GetAsync(int contact_id) => _repo.GetAsync(contact_id);
    public Task<IEnumerable<ContactDetails>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<ContactDetails?> RemoveAsync(int contact_id) => _repo.RemoveAsync(contact_id);
    public Task<ContactDetails?> UpdateAsync(int contact_id, ContactDetails item) => _repo.UpdateAsync(contact_id, item);
}