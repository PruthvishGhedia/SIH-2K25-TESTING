using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing hostels in the ERP system.
/// This controller provides CRUD operations for hostel facilities and accommodation management.
/// </summary>
[ApiController]
[Route("api/hostel")]
public class HostelController : BaseController
{
    private readonly IHostelRepository _hostelRepository;

    public HostelController(IHostelRepository hostelRepository)
    {
        _hostelRepository = hostelRepository;
    }

    /// <summary>
    /// Retrieves a list of hostel records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of hostel records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of hostel records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Hostel objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hostel>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var hostels = await _hostelRepository.ListAsync(limit, offset);
            return Ok(hostels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific hostel record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hostel record</param>
    /// <returns>The Hostel object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Hostel?>> GetAsync(int id)
    {
        try
        {
            var hostel = await _hostelRepository.GetAsync(id);
            if (hostel == null)
            {
                return NotFound($"Hostel record with ID {id} not found");
            }
            return Ok(hostel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new hostel record in the system.
    /// </summary>
    /// <param name="hostel">The hostel object to create</param>
    /// <returns>The created Hostel object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Hostel>> CreateAsync([FromBody] Hostel hostel)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(hostel.hostel_name))
            {
                return BadRequest("Hostel name is required");
            }

            if (string.IsNullOrWhiteSpace(hostel.type))
            {
                return BadRequest("Hostel type is required");
            }

            var createdHostel = await _hostelRepository.CreateAsync(hostel);
            return CreatedAtAction(nameof(GetAsync), new { id = createdHostel.hostel_id }, createdHostel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing hostel record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the hostel record to update</param>
    /// <param name="hostel">The hostel object with updated information</param>
    /// <returns>The updated Hostel object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Hostel?>> UpdateAsync(int id, [FromBody] Hostel hostel)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(hostel.hostel_name))
            {
                return BadRequest("Hostel name is required");
            }

            if (string.IsNullOrWhiteSpace(hostel.type))
            {
                return BadRequest("Hostel type is required");
            }

            var updatedHostel = await _hostelRepository.UpdateAsync(id, hostel);
            if (updatedHostel == null)
            {
                return NotFound($"Hostel record with ID {id} not found");
            }
            return Ok(updatedHostel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a hostel record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hostel record to remove</param>
    /// <returns>The removed Hostel object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Hostel?>> RemoveAsync(int id)
    {
        try
        {
            var removedHostel = await _hostelRepository.RemoveAsync(id);
            if (removedHostel == null)
            {
                return NotFound($"Hostel record with ID {id} not found");
            }
            return Ok(removedHostel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}