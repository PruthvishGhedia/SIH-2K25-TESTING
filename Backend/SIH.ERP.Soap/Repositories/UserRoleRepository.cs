using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class UserRoleRepository : RepositoryBase, IUserRoleRepository
{
    public UserRoleRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<UserRole>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<UserRole>("SELECT * FROM user_role ORDER BY user_role_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list user roles", ex);
        }
    }

    public async Task<UserRole?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<UserRole>("SELECT * FROM user_role WHERE \"user_role_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get user role with ID {id}", ex);
        }
    }

    public async Task<UserRole> CreateAsync(UserRole item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO user_role(\"user_role_id\", \"user_id\", \"role_id\") VALUES (@user_role_id, @user_id, @role_id) RETURNING *";
            return await _connection.QuerySingleAsync<UserRole>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create user role", ex);
        }
    }

    public async Task<UserRole?> UpdateAsync(int id, UserRole item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE user_role SET \"user_id\"=@user_id, \"role_id\"=@role_id WHERE \"user_role_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<UserRole>(sql, new { id, item.user_id, item.role_id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update user role with ID {id}", ex);
        }
    }

    public async Task<UserRole?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM user_role WHERE \"user_role_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<UserRole>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove user role with ID {id}", ex);
        }
    }
}