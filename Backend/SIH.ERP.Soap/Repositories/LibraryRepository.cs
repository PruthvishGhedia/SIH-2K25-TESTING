using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class LibraryRepository
{
    private readonly IDbConnection _db;
    public LibraryRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Library>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<Library>("SELECT * FROM library ORDER BY book_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<Library?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<Library>("SELECT * FROM library WHERE \"book_id\"=@id", new { id });
    }

    public async Task<Library> CreateAsync(Library item)
    {
        var sql = "INSERT INTO library(\"book_id\", \"title\", \"author\", \"shelf\", \"isbn\", \"copies\") VALUES (@book_id, @title, @author, @shelf, @isbn, @copies) RETURNING *";
        return await _db.QuerySingleAsync<Library>(sql, item);
    }

    public async Task<Library?> UpdateAsync(int id, Library item)
    {
        var sql = "UPDATE library SET \"title\"=@title, \"author\"=@author, \"shelf\"=@shelf, \"isbn\"=@isbn, \"copies\"=@copies WHERE \"book_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Library>(sql, new { id, item.title, item.author, item.shelf, item.isbn, item.copies });
    }

    public async Task<Library?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM library WHERE \"book_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<Library>(sql, new { id });
    }
}