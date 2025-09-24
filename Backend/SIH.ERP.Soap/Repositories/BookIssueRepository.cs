using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public class BookIssueRepository
{
    private readonly IDbConnection _db;
    public BookIssueRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<IEnumerable<BookIssue>> ListAsync(int limit, int offset)
    {
        return await _db.QueryAsync<BookIssue>("SELECT * FROM book_issue ORDER BY issue_id LIMIT @limit OFFSET @offset", new { limit, offset });
    }

    public async Task<BookIssue?> GetAsync(int id)
    {
        return await _db.QueryFirstOrDefaultAsync<BookIssue>("SELECT * FROM book_issue WHERE \"issue_id\"=@id", new { id });
    }

    public async Task<BookIssue> CreateAsync(BookIssue item)
    {
        var sql = "INSERT INTO book_issue(\"issue_id\", \"book_id\", \"student_id\", \"issue_date\", \"return_date\", \"status\") VALUES (@issue_id, @book_id, @student_id, @issue_date, @return_date, @status) RETURNING *";
        return await _db.QuerySingleAsync<BookIssue>(sql, item);
    }

    public async Task<BookIssue?> UpdateAsync(int id, BookIssue item)
    {
        var sql = "UPDATE book_issue SET \"book_id\"=@book_id, \"student_id\"=@student_id, \"issue_date\"=@issue_date, \"return_date\"=@return_date, \"status\"=@status WHERE \"issue_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<BookIssue>(sql, new { id, item.book_id, item.student_id, item.issue_date, item.return_date, item.status });
    }

    public async Task<BookIssue?> RemoveAsync(int id)
    {
        var sql = "DELETE FROM book_issue WHERE \"issue_id\"=@id RETURNING *";
        return await _db.QueryFirstOrDefaultAsync<BookIssue>(sql, new { id });
    }
}