using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing guardians in the ERP system.
/// This controller provides CRUD operations for student guardian information and family details.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GuardianController : BaseController
{
    private readonly IGuardianRepository _guardianRepository;

    public GuardianController(IGuardianRepository guardianRepository)
    {
        _guardianRepository = guardianRepository;
    }

    /// <summary>
    /// Retrieves a list of guardian records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of guardian records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of guardian records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Guardian objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guardian>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var guardians = await _guardianRepository.ListAsync(limit, offset);
            return Ok(guardians);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific guardian record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the guardian record</param>
    /// <returns>The Guardian object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Guardian?>> GetAsync(int id)
    {
        try
        {
            var guardian = await _guardianRepository.GetAsync(id);
            if (guardian == null)
            {
                return NotFound($"Guardian record with ID {id} not found");
            }
            return Ok(guardian);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new guardian record in the system.
    /// </summary>
    /// <param name="guardian">The guardian object to create</param>
    /// <returns>The created Guardian object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Guardian>> CreateAsync([FromBody] Guardian guardian)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(guardian.name))
            {
                return BadRequest("Name is required");
            }

            if (string.IsNullOrWhiteSpace(guardian.relationship))
            {
                return BadRequest("Relationship is required");
            }

            var createdGuardian = await _guardianRepository.CreateAsync(guardian);
            return CreatedAtAction(nameof(GetAsync), new { id = createdGuardian.guardian_id }, createdGuardian);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing guardian record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the guardian record to update</param>
    /// <param name="guardian">The guardian object with updated information</param>
    /// <returns>The updated Guardian object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Guardian?>> UpdateAsync(int id, [FromBody] Guardian guardian)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(guardian.name))
            {
                return BadRequest("Name is required");
            }

            if (string.IsNullOrWhiteSpace(guardian.relationship))
            {
                return BadRequest("Relationship is required");
            }

            var updatedGuardian = await _guardianRepository.UpdateAsync(id, guardian);
            if (updatedGuardian == null)
            {
                return NotFound($"Guardian record with ID {id} not found");
            }
            return Ok(updatedGuardian);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a guardian record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the guardian record to remove</param>
    /// <returns>The removed Guardian object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Guardian?>> RemoveAsync(int id)
    {
        try
        {
            var removedGuardian = await _guardianRepository.RemoveAsync(id);
            if (removedGuardian == null)
            {
                return NotFound($"Guardian record with ID {id} not found");
            }
            return Ok(removedGuardian);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}