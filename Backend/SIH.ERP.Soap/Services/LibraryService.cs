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

    public async Task<Library> CreateAsync(Library item)
    {
        return await _repo.CreateAsync(item);
    }
    
    public async Task<Library?> GetAsync(string book_id)
    {
        if (int.TryParse(book_id, out int id))
        {
            return await _repo.GetAsync(id);
        }
        return null;
    }
    
    public Task<IEnumerable<Library>> ListAsync(int limit = 100, int offset = 0) => _repo.ListAsync(limit, offset);
    
    public async Task<Library?> RemoveAsync(string book_id)
    {
        if (int.TryParse(book_id, out int id))
        {
            return await _repo.RemoveAsync(id);
        }
        return null;
    }
    
    public async Task<Library?> UpdateAsync(string book_id, Library item)
    {
        if (int.TryParse(book_id, out int id))
        {
            return await _repo.UpdateAsync(id, item);
        }
        return null;
    }
}