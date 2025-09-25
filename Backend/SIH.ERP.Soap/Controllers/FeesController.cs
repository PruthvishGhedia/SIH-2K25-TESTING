using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing fees in the educational institution.
/// Provides full CRUD operations for fees records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FeesController : ControllerBase
{
    private readonly IFeesRepository _feesRepository;

    /// <summary>
    /// Initializes a new instance of the FeesController class.
    /// </summary>
    /// <param name="feesRepository">The fees repository for data access.</param>
    public FeesController(IFeesRepository feesRepository)
    {
        _feesRepository = feesRepository;
    }

    /// <summary>
    /// Retrieves a list of fees records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of fees records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of fees records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Fees objects</returns>
    /// <response code="200">Returns the list of fees records</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fees>>> GetFees(int limit = 100, int offset = 0)
    {
        try
        {
            var fees = await _feesRepository.ListAsync(limit, offset);
            return Ok(fees);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific fees record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the fees record</param>
    /// <returns>The Fees object if found</returns>
    /// <response code="200">Returns the requested fees record</response>
    /// <response code="404">Fees record not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Fees>> GetFee(int id)
    {
        try
        {
            var fee = await _feesRepository.GetAsync(id);
            if (fee == null)
            {
                return NotFound($"Fees record with ID {id} not found.");
            }
            return Ok(fee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new fees record in the system.
    /// </summary>
    /// <param name="fee">The fees object to create</param>
    /// <returns>The created Fees object with assigned ID</returns>
    /// <response code="201">Returns the created fees record</response>
    /// <response code="400">Invalid fees data provided</response>
    [HttpPost]
    public async Task<ActionResult<Fees>> CreateFee([FromBody] Fees fee)
    {
        try
        {
            if (fee == null)
            {
                return BadRequest("Fees data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(fee.fee_type))
            {
                ModelState.AddModelError("FeeType", "Fee type is required.");
            }

            if (fee.amount <= 0)
            {
                ModelState.AddModelError("Amount", "Amount must be greater than zero.");
            }

            if (!fee.due_date.HasValue)
            {
                ModelState.AddModelError("DueDate", "Due date is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdFee = await _feesRepository.CreateAsync(fee);
            return CreatedAtAction(nameof(GetFee), new { id = createdFee.fee_id }, createdFee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing fees record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the fees record to update</param>
    /// <param name="fee">The fees object with updated information</param>
    /// <returns>The updated Fees object if successful</returns>
    /// <response code="200">Returns the updated fees record</response>
    /// <response code="400">Invalid fees data provided</response>
    /// <response code="404">Fees record not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Fees>> UpdateFee(int id, [FromBody] Fees fee)
    {
        try
        {
            if (fee == null)
            {
                return BadRequest("Fees data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(fee.fee_type))
            {
                ModelState.AddModelError("FeeType", "Fee type is required.");
            }

            if (fee.amount <= 0)
            {
                ModelState.AddModelError("Amount", "Amount must be greater than zero.");
            }

            if (!fee.due_date.HasValue)
            {
                ModelState.AddModelError("DueDate", "Due date is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFee = await _feesRepository.GetAsync(id);
            if (existingFee == null)
            {
                return NotFound($"Fees record with ID {id} not found.");
            }

            var updatedFee = await _feesRepository.UpdateAsync(id, fee);
            if (updatedFee == null)
            {
                return NotFound($"Fees record with ID {id} could not be updated.");
            }

            return Ok(updatedFee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a fees record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the fees record to remove</param>
    /// <returns>Success status if the fees record was removed</returns>
    /// <response code="204">Fees record successfully removed</response>
    /// <response code="404">Fees record not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFee(int id)
    {
        try
        {
            var fee = await _feesRepository.GetAsync(id);
            if (fee == null)
            {
                return NotFound($"Fees record with ID {id} not found.");
            }

            var removedFee = await _feesRepository.RemoveAsync(id);
            if (removedFee == null)
            {
                return NotFound($"Fees record with ID {id} could not be removed.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}