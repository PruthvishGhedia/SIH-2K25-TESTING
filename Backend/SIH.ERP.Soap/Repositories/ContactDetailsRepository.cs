using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class ContactDetailsRepository : RepositoryBase, IContactDetailsRepository
{
    public ContactDetailsRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<ContactDetails>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<ContactDetails>("SELECT * FROM contact_details ORDER BY contact_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list contact details", ex);
        }
    }

    public async Task<ContactDetails?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<ContactDetails>("SELECT * FROM contact_details WHERE \"contact_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get contact details with ID {id}", ex);
        }
    }

    public async Task<ContactDetails> CreateAsync(ContactDetails item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO contact_details(\"contact_id\", \"user_id\", \"contact_type\", \"contact_value\", \"is_primary\") VALUES (@contact_id, @user_id, @contact_type, @contact_value, @is_primary) RETURNING *";
            return await _connection.QuerySingleAsync<ContactDetails>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create contact details", ex);
        }
    }

    public async Task<ContactDetails?> UpdateAsync(int id, ContactDetails item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE contact_details SET \"user_id\"=@user_id, \"contact_type\"=@contact_type, \"contact_value\"=@contact_value, \"is_primary\"=@is_primary WHERE \"contact_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<ContactDetails>(sql, new { id, item.user_id, item.contact_type, item.contact_value, item.is_primary });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update contact details with ID {id}", ex);
        }
    }

    public async Task<ContactDetails?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM contact_details WHERE \"contact_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<ContactDetails>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove contact details with ID {id}", ex);
        }
    }
}