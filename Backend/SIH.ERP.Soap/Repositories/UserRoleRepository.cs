using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class UserRoleRepository
{
    private readonly IDbConnection _db;
    public UserRoleRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UserRole>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<UserRole>("SELECT * FROM user_role ORDER BY user_role_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<UserRole?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<UserRole>("SELECT * FROM user_role WHERE \"user_role_id\"=@id", new { id });
    }

    public async Task<UserRole> CreateAsync(UserRole item)
    {
        var sql = "INSERT INTO user_role(\"user_role_id\", \"user_id\", \"role_id\") VALUES (@user_role_id, @user_id, @role_id) RETURNING *";
        return await _db.QuerySingleAsync<UserRole>(sql, item);
    }

    public async Task<UserRole?> UpdateAsync(int id, UserRole item)
    {
        var sql = "UPDATE user_role SET \"user_id\"=@user_id, \"role_id\"=@role_id WHERE \"user_role_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<UserRole>(sql, new { id, item.user_id, item.role_id });
    }

    public async Task<UserRole?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM user_role WHERE \"user_role_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<UserRole>(sql, new { id });
    }
}