using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class HostelAllocationRepository
{
    private readonly IDbConnection _db;
    public HostelAllocationRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<HostelAllocation>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<HostelAllocation>("SELECT * FROM hostel_allocation ORDER BY allocation_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<HostelAllocation?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<HostelAllocation>("SELECT * FROM hostel_allocation WHERE \"allocation_id\"=@id", new { id });
    }

    public async Task<HostelAllocation> CreateAsync(HostelAllocation item)
    {
        var sql = "INSERT INTO hostel_allocation(\"allocation_id\", \"student_id\", \"hostel_id\", \"room_id\", \"start_date\", \"end_date\", \"status\", \"created_at\", \"updated_at\") VALUES (@allocation_id, @student_id, @hostel_id, @room_id, @start_date, @end_date, @status, @created_at, @updated_at) RETURNING *";
        return await _db.QuerySingleAsync<HostelAllocation>(sql, item);
    }

    public async Task<HostelAllocation?> UpdateAsync(int id, HostelAllocation item)
    {
        var sql = "UPDATE hostel_allocation SET \"student_id\"=@student_id, \"hostel_id\"=@hostel_id, \"room_id\"=@room_id, \"start_date\"=@start_date, \"end_date\"=@end_date, \"status\"=@status, \"updated_at\"=@updated_at WHERE \"allocation_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<HostelAllocation>(sql, new { id, item.student_id, item.hostel_id, item.room_id, item.start_date, item.end_date, item.status, item.updated_at });
    }

    public async Task<HostelAllocation?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM hostel_allocation WHERE \"allocation_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<HostelAllocation>(sql, new { id });
    }
}