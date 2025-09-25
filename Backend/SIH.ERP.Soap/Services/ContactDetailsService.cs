using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;

namespace SIH.ERP.Soap.Services;

public class ContactDetailsService : IContactDetailsService
{
    private readonly ContactDetailsRepository _repo;
    private readonly IHubContext<DashboardHub> _hubContext;
    
    public ContactDetailsService(ContactDetailsRepository repo, IHubContext<DashboardHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    public async Task<ContactDetails> CreateAsync(ContactDetails item)
    {
        var contactDetails = await _repo.CreateAsync(item);
        
        // Send real-time update
        await _hubContext.Clients.All.SendAsync("ReceiveContactDetailsUpdate", contactDetails);
        
        return contactDetails;
    }
    
    public async Task<ContactDetails?> GetAsync(string contact_id)
    {
        if (int.TryParse(contact_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<ContactDetails>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<ContactDetails?> RemoveAsync(string contact_id)
    {
        if (int.TryParse(contact_id, out int id))
        {
            var contactDetails = await _repo.RemoveAsync(id);
            
            // Send real-time update
            if (contactDetails != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveContactDetailsUpdate", contactDetails);
            }
            
            return contactDetails;
        }
        return null;
    }
    
    public async Task<ContactDetails?> UpdateAsync(string contact_id, ContactDetails item)
    {
        if (int.TryParse(contact_id, out int id))
        {
            var contactDetails = await _repo.UpdateAsync(id, item);
            
            // Send real-time update
            if (contactDetails != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveContactDetailsUpdate", contactDetails);
            }
            
            return contactDetails;
        }
        return null;
    }
}