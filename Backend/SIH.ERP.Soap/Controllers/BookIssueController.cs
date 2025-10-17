using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing book issues in the ERP system.
/// This controller provides CRUD operations for library book lending and return management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookIssueController : BaseController
{
    private readonly IBookIssueRepository _bookIssueRepository;

    public BookIssueController(IBookIssueRepository bookIssueRepository)
    {
        _bookIssueRepository = bookIssueRepository;
    }

    /// <summary>
    /// Retrieves a list of book issue records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of book issue records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of book issue records to skip for pagination (default: 0)</param>
    /// <returns>A collection of BookIssue objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookIssue>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var issues = await _bookIssueRepository.ListAsync(limit, offset);
            return Ok(issues);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific book issue record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book issue record</param>
    /// <returns>The BookIssue object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookIssue?>> GetAsync(int id)
    {
        try
        {
            var issue = await _bookIssueRepository.GetAsync(id);
            if (issue == null)
            {
                return NotFound($"Book issue record with ID {id} not found");
            }
            return Ok(issue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new book issue record in the system.
    /// </summary>
    /// <param name="issue">The book issue object to create</param>
    /// <returns>The created BookIssue object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<BookIssue>> CreateAsync([FromBody] BookIssue issue)
    {
        try
        {
            // Validate required fields
            if (issue.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (issue.book_id <= 0)
            {
                return BadRequest("Book ID is required and must be greater than 0");
            }

            if (issue.issue_date == default(DateTime))
            {
                return BadRequest("Issue date is required");
            }

            var createdIssue = await _bookIssueRepository.CreateAsync(issue);
            return CreatedAtAction(nameof(GetAsync), new { id = createdIssue.issue_id }, createdIssue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing book issue record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the book issue record to update</param>
    /// <param name="issue">The book issue object with updated information</param>
    /// <returns>The updated BookIssue object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<BookIssue?>> UpdateAsync(int id, [FromBody] BookIssue issue)
    {
        try
        {
            // Validate required fields
            if (issue.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (issue.book_id <= 0)
            {
                return BadRequest("Book ID is required and must be greater than 0");
            }

            if (issue.issue_date == default(DateTime))
            {
                return BadRequest("Issue date is required");
            }

            var updatedIssue = await _bookIssueRepository.UpdateAsync(id, issue);
            if (updatedIssue == null)
            {
                return NotFound($"Book issue record with ID {id} not found");
            }
            return Ok(updatedIssue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a book issue record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the book issue record to remove</param>
    /// <returns>The removed BookIssue object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<BookIssue?>> RemoveAsync(int id)
    {
        try
        {
            var removedIssue = await _bookIssueRepository.RemoveAsync(id);
            if (removedIssue == null)
            {
                return NotFound($"Book issue record with ID {id} not found");
            }
            return Ok(removedIssue);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}