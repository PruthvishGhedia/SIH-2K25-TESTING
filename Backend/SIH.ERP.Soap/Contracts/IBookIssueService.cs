using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface IBookIssueService
{
    [OperationContract]
    Task<IEnumerable<BookIssue>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<BookIssue?> GetAsync(int issue_id);

    [OperationContract]
    Task<BookIssue> CreateAsync(BookIssue item);

    [OperationContract]
    Task<BookIssue?> UpdateAsync(int issue_id, BookIssue item);

    [OperationContract]
    Task<BookIssue?> RemoveAsync(int issue_id);
}