using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class ResultService : IResultService
{
    private readonly ResultRepository _repo;
    public ResultService(ResultRepository repo)
    {
        _repo = repo;
    }

    public Task<Result> CreateAsync(Result item) => _repo.CreateAsync(item);
    public Task<Result?> GetAsync(int result_id) => _repo.GetAsync(result_id);
    public Task<IEnumerable<Result>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Result?> RemoveAsync(int result_id) => _repo.RemoveAsync(result_id);
    public Task<Result?> UpdateAsync(int result_id, Result item) => _repo.UpdateAsync(result_id, item);
}