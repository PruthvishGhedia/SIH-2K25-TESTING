using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class RoleRepository : RepositoryBase, IRoleRepository
{
    public RoleRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Role>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Role>("SELECT * FROM role ORDER BY role_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list roles", ex);
        }
    }

    public async Task<Role?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Role>("SELECT * FROM role WHERE \"role_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get role with ID {id}", ex);
        }
    }

    public async Task<Role> CreateAsync(Role item)
    {
        try
        {
            EnsureConnection();
            return await _connection.QuerySingleAsync<Role>("INSERT INTO role(\"role_id\",\"role_name\") VALUES (@role_id,@role_name) RETURNING *", item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create role", ex);
        }
    }

    public async Task<Role?> UpdateAsync(int id, Role item)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Role>("UPDATE role SET \"role_name\"=@role_name WHERE \"role_id\"=@id RETURNING *", new { id, item.role_name });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update role with ID {id}", ex);
        }
    }

    public async Task<Role?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Role>("DELETE FROM role WHERE \"role_id\"=@id RETURNING *", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove role with ID {id}", ex);
        }
    }
}