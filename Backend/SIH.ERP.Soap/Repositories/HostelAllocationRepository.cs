using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class HostelAllocationRepository : RepositoryBase, IHostelAllocationRepository
{
    public HostelAllocationRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<HostelAllocation>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<HostelAllocation>("SELECT * FROM hostel_allocation ORDER BY allocation_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list hostel allocations", ex);
        }
    }

    public async Task<HostelAllocation?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<HostelAllocation>("SELECT * FROM hostel_allocation WHERE \"allocation_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get hostel allocation with ID {id}", ex);
        }
    }

    public async Task<HostelAllocation> CreateAsync(HostelAllocation item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO hostel_allocation(\"allocation_id\", \"student_id\", \"hostel_id\", \"room_id\", \"start_date\", \"end_date\", \"status\", \"created_at\", \"updated_at\") VALUES (@allocation_id, @student_id, @hostel_id, @room_id, @start_date, @end_date, @status, @created_at, @updated_at) RETURNING *";
            return await _connection.QuerySingleAsync<HostelAllocation>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create hostel allocation", ex);
        }
    }

    public async Task<HostelAllocation?> UpdateAsync(int id, HostelAllocation item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE hostel_allocation SET \"student_id\"=@student_id, \"hostel_id\"=@hostel_id, \"room_id\"=@room_id, \"start_date\"=@start_date, \"end_date\"=@end_date, \"status\"=@status, \"updated_at\"=@updated_at WHERE \"allocation_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<HostelAllocation>(sql, new { id, item.student_id, item.hostel_id, item.room_id, item.start_date, item.end_date, item.status, item.updated_at });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update hostel allocation with ID {id}", ex);
        }
    }

    public async Task<HostelAllocation?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM hostel_allocation WHERE \"allocation_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<HostelAllocation>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove hostel allocation with ID {id}", ex);
        }
    }
}