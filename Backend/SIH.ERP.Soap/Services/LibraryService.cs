using CoreWCF;
using SIH.ERP.Soap.Contracts;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Services;

public class LibraryService : ILibraryService
{
    private readonly LibraryRepository _repo;
    public LibraryService(LibraryRepository repo)
    {
        _repo = repo;
    }

    public Task<Library> CreateAsync(Library item) => _repo.CreateAsync(item);
    public Task<Library?> GetAsync(int book_id) => _repo.GetAsync(book_id);
    public Task<IEnumerable<Library>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    public Task<Library?> RemoveAsync(int book_id) => _repo.RemoveAsync(book_id);
    public Task<Library?> UpdateAsync(int book_id, Library item) => _repo.UpdateAsync(book_id, item);
}