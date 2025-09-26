using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class HostelRepository : RepositoryBase, IHostelRepository
{
    public HostelRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Hostel>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Hostel>("SELECT * FROM hostel ORDER BY hostel_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list hostels", ex);
        }
    }

    public async Task<Hostel?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Hostel>("SELECT * FROM hostel WHERE \"hostel_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get hostel with ID {id}", ex);
        }
    }

    public async Task<Hostel> CreateAsync(Hostel item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO hostel(\"hostel_id\", \"hostel_name\", \"type\") VALUES (@hostel_id, @hostel_name, @type) RETURNING *";
            return await _connection.QuerySingleAsync<Hostel>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create hostel", ex);
        }
    }

    public async Task<Hostel?> UpdateAsync(int id, Hostel item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE hostel SET \"hostel_name\"=@hostel_name, \"type\"=@type WHERE \"hostel_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Hostel>(sql, new { id, item.hostel_name, item.type });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update hostel with ID {id}", ex);
        }
    }

    public async Task<Hostel?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM hostel WHERE \"hostel_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Hostel>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove hostel with ID {id}", ex);
        }
    }
}