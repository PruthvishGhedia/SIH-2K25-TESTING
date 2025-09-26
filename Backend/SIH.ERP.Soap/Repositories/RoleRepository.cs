using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class RoleRepository
{
    private readonly IDbConnection _db;
    public RoleRepository(IDbConnection db) { _db = db; }

    public Task<IEnumerable<Role>> ListAsync(int limit, int offset) => _db.QueryAsync<Role>("SELECT * FROM role ORDER BY role_id LIMIT @limit OFFSET @offset", new { limit, offset });
    public Task<Role?> GetAsync(int id) => _db.QueryFirstOrDefaultAsync<Role>("SELECT * FROM role WHERE \"role_id\"=@id", new { id });
    public Task<Role> CreateAsync(Role item) => _db.QuerySingleAsync<Role>("INSERT INTO role(\"role_id\",\"role_name\") VALUES (@role_id,@role_name) RETURNING *", item);
    public Task<Role?> UpdateAsync(int id, Role item) => _db.QueryFirstOrDefaultAsync<Role>("UPDATE role SET \"role_name\"=@role_name WHERE \"role_id\"=@id RETURNING *", new { id, item.role_name });
    public Task<Role?> RemoveAsync(int id) => _db.QueryFirstOrDefaultAsync<Role>("DELETE FROM role WHERE \"role_id\"=@id RETURNING *", new { id });
}

