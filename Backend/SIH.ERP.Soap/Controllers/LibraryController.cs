using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing library items in the ERP system.
/// This controller provides CRUD operations for library books and resources management.
/// </summary>
[ApiController]
[Route("api/library/books")]
public class LibraryController : BaseController
{
    private readonly ILibraryRepository _libraryRepository;

    public LibraryController(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    /// <summary>
    /// Retrieves a list of library items with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of library items to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of library items to skip for pagination (default: 0)</param>
    /// <returns>A collection of Library objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Library>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var books = await _libraryRepository.ListAsync(limit, offset);
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific library item by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the library item</param>
    /// <returns>The Library object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Library?>> GetAsync(int id)
    {
        try
        {
            var book = await _libraryRepository.GetAsync(id);
            if (book == null)
            {
                return NotFound($"Library item with ID {id} not found");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new library item in the system.
    /// </summary>
    /// <param name="book">The library item object to create</param>
    /// <returns>The created Library object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Library>> CreateAsync([FromBody] Library book)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(book.title))
            {
                return BadRequest("Title is required");
            }

            if (string.IsNullOrWhiteSpace(book.author))
            {
                return BadRequest("Author is required");
            }

            var createdBook = await _libraryRepository.CreateAsync(book);
            return CreatedAtAction(nameof(GetAsync), new { id = createdBook.book_id }, createdBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing library item with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the library item to update</param>
    /// <param name="book">The library item object with updated information</param>
    /// <returns>The updated Library object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Library?>> UpdateAsync(int id, [FromBody] Library book)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(book.title))
            {
                return BadRequest("Title is required");
            }

            if (string.IsNullOrWhiteSpace(book.author))
            {
                return BadRequest("Author is required");
            }

            var updatedBook = await _libraryRepository.UpdateAsync(id, book);
            if (updatedBook == null)
            {
                return NotFound($"Library item with ID {id} not found");
            }
            return Ok(updatedBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a library item from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the library item to remove</param>
    /// <returns>The removed Library object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Library?>> RemoveAsync(int id)
    {
        try
        {
            var removedBook = await _libraryRepository.RemoveAsync(id);
            if (removedBook == null)
            {
                return NotFound($"Library item with ID {id} not found");
            }
            return Ok(removedBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}