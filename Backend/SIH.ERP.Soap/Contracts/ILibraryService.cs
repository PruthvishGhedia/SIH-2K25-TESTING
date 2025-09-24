using CoreWCF;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

[ServiceContract]
public interface ILibraryService
{
    [OperationContract]
    Task<IEnumerable<Library>> ListAsync(int limit = 100, int offset = 0);

    [OperationContract]
    Task<Library?> GetAsync(int book_id);

    [OperationContract]
    Task<Library> CreateAsync(Library item);

    [OperationContract]
    Task<Library?> UpdateAsync(int book_id, Library item);

    [OperationContract]
    Task<Library?> RemoveAsync(int book_id);
}