using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class UserService : IUserService
{
    private readonly UserRepository _repo;
    public UserService(UserRepository repo)
    {
        _repo = repo;
    }

    public Task<User> CreateAsync(User item) => _repo.CreateAsync(item);
    public Task<User?> GetAsync(int user_id) => _repo.GetAsync(user_id);
    public Task<IEnumerable<User>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<User?> RemoveAsync(int user_id) => _repo.RemoveAsync(user_id);
    public Task<User?> UpdateAsync(int user_id, User item) => _repo.UpdateAsync(user_id, item);
}