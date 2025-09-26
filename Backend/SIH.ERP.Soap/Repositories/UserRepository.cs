using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<User>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<User>("SELECT * FROM usr ORDER BY user_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list users", ex);
        }
    }

    public async Task<User?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM usr WHERE \"user_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get user with ID {id}", ex);
        }
    }

    public async Task<User> CreateAsync(User item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO usr(\"user_id\", \"full_name\", \"email\", \"dob\", \"password_hash\", \"is_active\", \"created_at\", \"updated_at\") VALUES (@user_id, @full_name, @email, @dob, @password_hash, @is_active, @created_at, @updated_at) RETURNING *";
            return await _connection.QuerySingleAsync<User>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create user", ex);
        }
    }

    public async Task<User?> UpdateAsync(int id, User item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE usr SET \"full_name\"=@full_name, \"email\"=@email, \"dob\"=@dob, \"password_hash\"=@password_hash, \"is_active\"=@is_active, \"updated_at\"=@updated_at WHERE \"user_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { id, item.full_name, item.email, item.dob, item.password_hash, item.is_active, item.updated_at });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update user with ID {id}", ex);
        }
    }

    public async Task<User?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM usr WHERE \"user_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove user with ID {id}", ex);
        }
    }
}