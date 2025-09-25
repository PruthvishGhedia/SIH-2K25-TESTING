using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class HostelService : IHostelService
{
    private readonly HostelRepository _repo;
    public HostelService(HostelRepository repo)
    {
        _repo = repo;
    }

    public async Task<Hostel> CreateAsync(Hostel item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<Hostel?> GetAsync(string hostel_id)
    {
        if (int.TryParse(hostel_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Hostel>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Hostel?> RemoveAsync(string hostel_id)
    {
        if (int.TryParse(hostel_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<Hostel?> UpdateAsync(string hostel_id, Hostel item)
    {
        if (int.TryParse(hostel_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}