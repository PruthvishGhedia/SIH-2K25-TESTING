using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync(int limit, int offset);
    Task<User?> GetAsync(int id);
    Task<User> CreateAsync(User item);
    Task<User?> UpdateAsync(int id, User item);
    Task<User?> RemoveAsync(int id);
}

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;
    public UserRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<User>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<User>("SELECT * FROM usr ORDER BY user_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<User?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM usr WHERE \"user_id\"=@id", new { id });
    }

    public async Task<User> CreateAsync(User item)
    {
        var sql = "INSERT INTO usr(\"user_id\", \"full_name\", \"email\", \"dob\", \"password_hash\", \"is_active\", \"created_at\", \"updated_at\") VALUES (@user_id, @full_name, @email, @dob, @password_hash, @is_active, @created_at, @updated_at) RETURNING *";
        return await _db.QuerySingleAsync<User>(sql, item);
    }

    public async Task<User?> UpdateAsync(int id, User item)
    {
        var sql = "UPDATE usr SET \"full_name\"=@full_name, \"email\"=@email, \"dob\"=@dob, \"password_hash\"=@password_hash, \"is_active\"=@is_active, \"updated_at\"=@updated_at WHERE \"user_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<User>(sql, new { id, item.full_name, item.email, item.dob, item.password_hash, item.is_active, item.updated_at });
    }

    public async Task<User?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM usr WHERE \"user_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<User>(sql, new { id });
    }
}