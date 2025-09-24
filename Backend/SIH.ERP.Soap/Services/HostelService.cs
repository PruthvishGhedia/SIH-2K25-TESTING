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

    public Task<Hostel> CreateAsync(Hostel item) => _repo.CreateAsync(item);
    public Task<Hostel?> GetAsync(int hostel_id) => _repo.GetAsync(hostel_id);
    public Task<IEnumerable<Hostel>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Hostel?> RemoveAsync(int hostel_id) => _repo.RemoveAsync(hostel_id);
    public Task<Hostel?> UpdateAsync(int hostel_id, Hostel item) => _repo.UpdateAsync(hostel_id, item);
}