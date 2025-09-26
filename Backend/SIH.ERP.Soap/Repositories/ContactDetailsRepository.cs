using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class ContactDetailsRepository
{
    private readonly IDbConnection _db;
    public ContactDetailsRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ContactDetails>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<ContactDetails>("SELECT * FROM contact_details ORDER BY contact_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<ContactDetails?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<ContactDetails>("SELECT * FROM contact_details WHERE \"contact_id\"=@id", new { id });
    }

    public async Task<ContactDetails> CreateAsync(ContactDetails item)
    {
        var sql = "INSERT INTO contact_details(\"contact_id\", \"user_id\", \"contact_type\", \"contact_value\", \"is_primary\") VALUES (@contact_id, @user_id, @contact_type, @contact_value, @is_primary) RETURNING *";
        return await _db.QuerySingleAsync<ContactDetails>(sql, item);
    }

    public async Task<ContactDetails?> UpdateAsync(int id, ContactDetails item)
    {
        var sql = "UPDATE contact_details SET \"user_id\"=@user_id, \"contact_type\"=@contact_type, \"contact_value\"=@contact_value, \"is_primary\"=@is_primary WHERE \"contact_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<ContactDetails>(sql, new { id, item.user_id, item.contact_type, item.contact_value, item.is_primary });
    }

    public async Task<ContactDetails?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM contact_details WHERE \"contact_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<ContactDetails>(sql, new { id });
    }
}