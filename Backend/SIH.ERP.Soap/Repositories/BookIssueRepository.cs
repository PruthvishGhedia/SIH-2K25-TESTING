using Dapper;
using SIH.ERP.Soap.Models;
using System.Data;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Repositories;

public class BookIssueRepository : RepositoryBase, IBookIssueRepository
{
    public BookIssueRepository(IDbConnection connection) : base(connection) { }

    public async Task<IEnumerable<BookIssue>> ListAsync(int limit, int offset)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryAsync<BookIssue>("SELECT * FROM book_issue ORDER BY issue_id LIMIT @limit OFFSET @offset", new { limit, offset });
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to list book issues", ex);
        }
    }

    public async Task<BookIssue?> GetAsync(int id)
    {
        try
        {
            EnsureConnection();
            return await _connection.QueryFirstOrDefaultAsync<BookIssue>("SELECT * FROM book_issue WHERE \"issue_id\"=@id", new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to get book issue with ID {id}", ex);
        }
    }

    public async Task<BookIssue> CreateAsync(BookIssue item)
    {
        try
        {
            EnsureConnection();
            var sql = "INSERT INTO book_issue(\"issue_id\", \"book_id\", \"student_id\", \"issue_date\", \"return_date\", \"status\") VALUES (@issue_id, @book_id, @student_id, @issue_date, @return_date, @status) RETURNING *";
            return await _connection.QuerySingleAsync<BookIssue>(sql, item);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Failed to create book issue", ex);
        }
    }

    public async Task<BookIssue?> UpdateAsync(int id, BookIssue item)
    {
        try
        {
            EnsureConnection();
            var sql = "UPDATE book_issue SET \"book_id\"=@book_id, \"student_id\"=@student_id, \"issue_date\"=@issue_date, \"return_date\"=@return_date, \"status\"=@status WHERE \"issue_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<BookIssue>(sql, new { id, item.book_id, item.student_id, item.issue_date, item.return_date, item.status });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to update book issue with ID {id}", ex);
        }
    }

    public async Task<BookIssue?> RemoveAsync(int id)
    {
        try
        {
            EnsureConnection();
            var sql = "DELETE FROM book_issue WHERE \"issue_id\"=@id RETURNING *";
            return await _connection.QueryFirstOrDefaultAsync<BookIssue>(sql, new { id });
        }
        catch (Exception ex)
        {
            throw new RepositoryException($"Failed to remove book issue with ID {id}", ex);
        }
    }
}