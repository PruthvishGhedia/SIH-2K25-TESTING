using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> ListAsync(int limit, int offset);
    Task<Department?> GetAsync(int id);
    Task<Department> CreateAsync(Department item);
    Task<Department?> UpdateAsync(int id, Department item);
    Task<Department?> RemoveAsync(int id);
}

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDbConnection _db;
    public DepartmentRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Department>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Department>("SELECT * FROM department ORDER BY dept_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Department?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Department>("SELECT * FROM department WHERE \"dept_id\"=@id", new { id });
    }

    public async Task<Department> CreateAsync(Department item)
    {
        var sql = "INSERT INTO department(\"dept_id\", \"dept_name\") VALUES (@dept_id, @dept_name) RETURNING *";
        return await _db.QuerySingleAsync<Department>(sql, item);
    }

    public async Task<Department?> UpdateAsync(int id, Department item)
    {
        var sql = "UPDATE department SET \"dept_name\"=@dept_name WHERE \"dept_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Department>(sql, new { id, item.dept_name });
    }

    public async Task<Department?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM department WHERE \"dept_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Department>(sql, new { id });
    }
}