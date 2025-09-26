using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing faculty members in the ERP system.
/// This controller provides CRUD operations for academic faculty information and teaching assignments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FacultyController : BaseController
{
    private readonly IFacultyRepository _facultyRepository;

    public FacultyController(IFacultyRepository facultyRepository)
    {
        _facultyRepository = facultyRepository;
    }

    /// <summary>
    /// Retrieves a list of faculty records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of faculty records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of faculty records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Faculty objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Faculty>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var faculties = await _facultyRepository.ListAsync(limit, offset);
            return Ok(faculties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific faculty record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the faculty record</param>
    /// <returns>The Faculty object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Faculty?>> GetAsync(int id)
    {
        try
        {
            var faculty = await _facultyRepository.GetAsync(id);
            if (faculty == null)
            {
                return NotFound($"Faculty record with ID {id} not found");
            }
            return Ok(faculty);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new faculty record in the system.
    /// </summary>
    /// <param name="faculty">The faculty object to create</param>
    /// <returns>The created Faculty object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Faculty>> CreateAsync([FromBody] Faculty faculty)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(faculty.first_name))
            {
                return BadRequest("First name is required");
            }

            if (string.IsNullOrWhiteSpace(faculty.last_name))
            {
                return BadRequest("Last name is required");
            }

            if (string.IsNullOrWhiteSpace(faculty.email))
            {
                return BadRequest("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(faculty.email))
            {
                return BadRequest("Email format is invalid");
            }

            var createdFaculty = await _facultyRepository.CreateAsync(faculty);
            return CreatedAtAction(nameof(GetAsync), new { id = createdFaculty.faculty_id }, createdFaculty);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing faculty record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the faculty record to update</param>
    /// <param name="faculty">The faculty object with updated information</param>
    /// <returns>The updated Faculty object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Faculty?>> UpdateAsync(int id, [FromBody] Faculty faculty)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(faculty.first_name))
            {
                return BadRequest("First name is required");
            }

            if (string.IsNullOrWhiteSpace(faculty.last_name))
            {
                return BadRequest("Last name is required");
            }

            if (string.IsNullOrWhiteSpace(faculty.email))
            {
                return BadRequest("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(faculty.email))
            {
                return BadRequest("Email format is invalid");
            }

            var updatedFaculty = await _facultyRepository.UpdateAsync(id, faculty);
            if (updatedFaculty == null)
            {
                return NotFound($"Faculty record with ID {id} not found");
            }
            return Ok(updatedFaculty);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a faculty record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the faculty record to remove</param>
    /// <returns>The removed Faculty object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Faculty?>> RemoveAsync(int id)
    {
        try
        {
            var removedFaculty = await _facultyRepository.RemoveAsync(id);
            if (removedFaculty == null)
            {
                return NotFound($"Faculty record with ID {id} not found");
            }
            return Ok(removedFaculty);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}