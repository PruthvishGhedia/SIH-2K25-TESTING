using SIH.ERP.Soap.Models;
using System.Data;

namespace SIH.ERP.Soap.Repositories;

public interface ILibraryRepository : IRepository
{
    Task<IEnumerable<Library>> ListAsync(int limit, int offset);
    Task<Library?> GetAsync(int id);
    Task<Library> CreateAsync(Library item);
    Task<Library?> UpdateAsync(int id, Library item);
    Task<Library?> RemoveAsync(int id);
}