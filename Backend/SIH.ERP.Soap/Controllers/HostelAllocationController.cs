using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing hostel allocations in the ERP system.
/// This controller provides CRUD operations for student hostel room assignments and allocation management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HostelAllocationController : BaseController
{
    private readonly IHostelAllocationRepository _hostelAllocationRepository;

    public HostelAllocationController(IHostelAllocationRepository hostelAllocationRepository)
    {
        _hostelAllocationRepository = hostelAllocationRepository;
    }

    /// <summary>
    /// Retrieves a list of hostel allocation records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of allocation records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of allocation records to skip for pagination (default: 0)</param>
    /// <returns>A collection of HostelAllocation objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HostelAllocation>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var allocations = await _hostelAllocationRepository.ListAsync(limit, offset);
            return Ok(allocations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific hostel allocation record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the allocation record</param>
    /// <returns>The HostelAllocation object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<HostelAllocation?>> GetAsync(int id)
    {
        try
        {
            var allocation = await _hostelAllocationRepository.GetAsync(id);
            if (allocation == null)
            {
                return NotFound($"Hostel allocation record with ID {id} not found");
            }
            return Ok(allocation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new hostel allocation record in the system.
    /// </summary>
    /// <param name="allocation">The hostel allocation object to create</param>
    /// <returns>The created HostelAllocation object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<HostelAllocation>> CreateAsync([FromBody] HostelAllocation allocation)
    {
        try
        {
            // Validate required fields
            if (allocation.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (allocation.hostel_id <= 0)
            {
                return BadRequest("Hostel ID is required and must be greater than 0");
            }

            if (allocation.room_id <= 0)
            {
                return BadRequest("Room ID is required and must be greater than 0");
            }

            if (allocation.start_date == default(DateTime))
            {
                return BadRequest("Start date is required");
            }

            if (string.IsNullOrWhiteSpace(allocation.status))
            {
                return BadRequest("Status is required");
            }

            var createdAllocation = await _hostelAllocationRepository.CreateAsync(allocation);
            return CreatedAtAction(nameof(GetAsync), new { id = createdAllocation.allocation_id }, createdAllocation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing hostel allocation record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the allocation record to update</param>
    /// <param name="allocation">The hostel allocation object with updated information</param>
    /// <returns>The updated HostelAllocation object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<HostelAllocation?>> UpdateAsync(int id, [FromBody] HostelAllocation allocation)
    {
        try
        {
            // Validate required fields
            if (allocation.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (allocation.hostel_id <= 0)
            {
                return BadRequest("Hostel ID is required and must be greater than 0");
            }

            if (allocation.room_id <= 0)
            {
                return BadRequest("Room ID is required and must be greater than 0");
            }

            if (allocation.start_date == default(DateTime))
            {
                return BadRequest("Start date is required");
            }

            if (string.IsNullOrWhiteSpace(allocation.status))
            {
                return BadRequest("Status is required");
            }

            var updatedAllocation = await _hostelAllocationRepository.UpdateAsync(id, allocation);
            if (updatedAllocation == null)
            {
                return NotFound($"Hostel allocation record with ID {id} not found");
            }
            return Ok(updatedAllocation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a hostel allocation record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the allocation record to remove</param>
    /// <returns>The removed HostelAllocation object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<HostelAllocation?>> RemoveAsync(int id)
    {
        try
        {
            var removedAllocation = await _hostelAllocationRepository.RemoveAsync(id);
            if (removedAllocation == null)
            {
                return NotFound($"Hostel allocation record with ID {id} not found");
            }
            return Ok(removedAllocation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}