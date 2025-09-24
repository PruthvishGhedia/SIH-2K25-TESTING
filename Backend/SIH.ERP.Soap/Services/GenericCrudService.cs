using CoreWCF;
using Dapper;
using System.Data;

namespace SIH.ERP.Soap.Services;

[ServiceContract]
public interface IGenericCrud
{
    [OperationContract]
    Task<List<Dictionary<string, object?>>> ListAsync(string table, int limit = 100, int offset = 0);

    [OperationContract]
    Task<Dictionary<string, object?>?> GetAsync(string table, string primaryKey, string id);

    [OperationContract]
    Task<Dictionary<string, object?>> CreateAsync(string table, Dictionary<string, object?> item);

    [OperationContract]
    Task<Dictionary<string, object?>?> UpdateAsync(string table, string primaryKey, string id, Dictionary<string, object?> item);

    [OperationContract]
    Task<Dictionary<string, object?>?> RemoveAsync(string table, string primaryKey, string id);
}

public class GenericCrudService : IGenericCrud
{
    private readonly IDbConnection _db;
    
    // Allowlisted tables for security
    private static readonly HashSet<string> _allowedTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "student", "course", "department", "user", "fees", "exam", 
        "guardian", "admission", "hostel", "room", "hostelallocation", 
        "library", "bookissue", "result", "userrole", "contactdetails",
        "role", "subject"
    };

    public GenericCrudService(IDbConnection db)
    {
        _db = db;
    }

    private void ValidateTable(string table)
    {
        if (!_allowedTables.Contains(table))
        {
            throw new ArgumentException($"Table '{table}' is not allowed. Allowed tables: {string.Join(", ", _allowedTables)}");
        }
    }

    public async Task<List<Dictionary<string, object?>>> ListAsync(string table, int limit = 100, int offset = 0)
    {
        ValidateTable(table);
        var sql = $"SELECT * FROM \"{table}\" ORDER BY 1 LIMIT @limit OFFSET @offset";
        var rows = await _db.QueryAsync(sql, new { limit, offset });
        return rows.Select(r => (IDictionary<string, object?>)r).Select(d => d.ToDictionary(k => k.Key, v => v.Value)).ToList();
    }

    public async Task<Dictionary<string, object?>?> GetAsync(string table, string primaryKey, string id)
    {
        ValidateTable(table);
        var sql = $"SELECT * FROM \"{table}\" WHERE \"{primaryKey}\"=@id";
        var row = await _db.QueryFirstOrDefaultAsync(sql, new { id });
        return row == null ? null : ((IDictionary<string, object?>)row).ToDictionary(k => k.Key, v => v.Value);
    }

    public async Task<Dictionary<string, object?>> CreateAsync(string table, Dictionary<string, object?> item)
    {
        ValidateTable(table);
        var keys = item.Keys.ToArray();
        var cols = string.Join(", ", keys.Select(k => $"\"{k}\""));
        var vals = string.Join(", ", keys.Select((_, i) => $"@p{i}"));
        var sql = $"INSERT INTO \"{table}\" ({cols}) VALUES ({vals}) RETURNING *";
        var param = new DynamicParameters();
        for (int i = 0; i < keys.Length; i++) param.Add($"p{i}", item[keys[i]]);
        var row = await _db.QuerySingleAsync(sql, param);
        return ((IDictionary<string, object?>)row).ToDictionary(k => k.Key, v => v.Value);
    }

    public async Task<Dictionary<string, object?>?> UpdateAsync(string table, string primaryKey, string id, Dictionary<string, object?> item)
    {
        ValidateTable(table);
        var keys = item.Keys.ToArray();
        var sets = string.Join(", ", keys.Select((k, i) => $"\"{k}\"=@p{i}"));
        var sql = $"UPDATE \"{table}\" SET {sets} WHERE \"{primaryKey}\"=@id RETURNING *";
        var param = new DynamicParameters();
        for (int i = 0; i < keys.Length; i++) param.Add($"p{i}", item[keys[i]]);
        param.Add("id", id);
        var row = await _db.QueryFirstOrDefaultAsync(sql, param);
        return row == null ? null : ((IDictionary<string, object?>)row).ToDictionary(k => k.Key, v => v.Value);
    }

    public async Task<Dictionary<string, object?>?> RemoveAsync(string table, string primaryKey, string id)
    {
        ValidateTable(table);
        var sql = $"DELETE FROM \"{table}\" WHERE \"{primaryKey}\"=@id RETURNING *";
        var row = await _db.QueryFirstOrDefaultAsync(sql, new { id });
        return row == null ? null : ((IDictionary<string, object?>)row).ToDictionary(k => k.Key, v => v.Value);
    }
}

