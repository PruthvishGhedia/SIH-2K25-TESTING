using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class FeesService : IFeesService
{
    private readonly FeesRepository _repo;
    public FeesService(FeesRepository repo)
    {
        _repo = repo;
    }

    public Task<Fees> CreateAsync(Fees item) => _repo.CreateAsync(item);
    public Task<Fees?> GetAsync(int fee_id) => _repo.GetAsync(fee_id);
    public Task<IEnumerable<Fees>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Fees?> RemoveAsync(int fee_id) => _repo.RemoveAsync(fee_id);
    public Task<Fees?> UpdateAsync(int fee_id, Fees item) => _repo.UpdateAsync(fee_id, item);
}