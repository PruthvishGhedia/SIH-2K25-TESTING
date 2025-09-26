using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing subjects in the ERP system.
/// This controller provides CRUD operations for academic subjects and course curriculum management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubjectController : BaseController
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectController(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    /// <summary>
    /// Retrieves a list of subject records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of subject records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of subject records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Subject objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var subjects = await _subjectRepository.ListAsync(limit, offset);
            return Ok(subjects);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific subject record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the subject record</param>
    /// <returns>The Subject object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject?>> GetAsync(int id)
    {
        try
        {
            var subject = await _subjectRepository.GetAsync(id);
            if (subject == null)
            {
                return NotFound($"Subject record with ID {id} not found");
            }
            return Ok(subject);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new subject record in the system.
    /// </summary>
    /// <param name="subject">The subject object to create</param>
    /// <returns>The created Subject object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Subject>> CreateAsync([FromBody] Subject subject)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(subject.subject_name))
            {
                return BadRequest("Subject name is required");
            }

            if (subject.subject_code <= 0)
            {
                return BadRequest("Subject code is required and must be greater than 0");
            }

            var createdSubject = await _subjectRepository.CreateAsync(subject);
            return CreatedAtAction(nameof(GetAsync), new { id = createdSubject.subject_code }, createdSubject);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing subject record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the subject record to update</param>
    /// <param name="subject">The subject object with updated information</param>
    /// <returns>The updated Subject object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Subject?>> UpdateAsync(int id, [FromBody] Subject subject)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(subject.subject_name))
            {
                return BadRequest("Subject name is required");
            }

            if (subject.subject_code <= 0)
            {
                return BadRequest("Subject code is required and must be greater than 0");
            }

            var updatedSubject = await _subjectRepository.UpdateAsync(id, subject);
            if (updatedSubject == null)
            {
                return NotFound($"Subject record with ID {id} not found");
            }
            return Ok(updatedSubject);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a subject record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the subject record to remove</param>
    /// <returns>The removed Subject object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Subject?>> RemoveAsync(int id)
    {
        try
        {
            var removedSubject = await _subjectRepository.RemoveAsync(id);
            if (removedSubject == null)
            {
                return NotFound($"Subject record with ID {id} not found");
            }
            return Ok(removedSubject);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}