using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class LibraryRepository : RepositoryBase, ILibraryRepository
{
    public LibraryRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<Library>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<Library>("SELECT * FROM library ORDER BY book_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list library books", ex);
        }
    }

    public async Task<Library?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<Library>("SELECT * FROM library WHERE \"book_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get library book with ID {id}", ex);
        }
    }

    public async Task<Library> CreateAsync(Library item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO library(\"book_id\", \"title\", \"author\", \"shelf\", \"isbn\", \"copies\") VALUES (@book_id, @title, @author, @shelf, @isbn, @copies) RETURNING *";
            return await _connection.QuerySingleAsync<Library>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create library book", ex);
        }
    }

    public async Task<Library?> UpdateAsync(int id, Library item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE library SET \"title\"=@title, \"author\"=@author, \"shelf\"=@shelf, \"isbn\"=@isbn, \"copies\"=@copies WHERE \"book_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Library>(sql, new { id, item.title, item.author, item.shelf, item.isbn, item.copies });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update library book with ID {id}", ex);
        }
    }

    public async Task<Library?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM library WHERE \"book_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<Library>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove library book with ID {id}", ex);
        }
    }
}