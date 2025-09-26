using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing student fees in the ERP system.
/// This controller provides CRUD operations for fee structures, payments, and payment tracking for students.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FeesController : BaseController
{
    private readonly IFeesRepository _feesRepository;

    public FeesController(IFeesRepository feesRepository)
    {
        _feesRepository = feesRepository;
    }

    /// <summary>
        /// Retrieves a list of fee records with pagination support.
        /// </summary>
        /// <param name="limit">Maximum number of fee records to retrieve (default: 100, maximum: 1000)</param>
        /// <param name="offset">Number of fee records to skip for pagination (default: 0)</param>
        /// <returns>A collection of Fees objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fees>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
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
    /// Retrieves a specific fee record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the fee record</param>
    /// <returns>The Fees object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Fees?>> GetAsync(int id)
    {
        try
        {
            var fee = await _feesRepository.GetAsync(id);
            if (fee == null)
            {
                return NotFound($"Fee record with ID {id} not found");
            }
            return Ok(fee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new fee record in the system.
    /// </summary>
    /// <param name="fee">The fee object to create</param>
    /// <returns>The created Fees object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Fees>> CreateAsync([FromBody] Fees fee)
    {
        try
        {
            // Validate required fields
            if (fee.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(fee.fee_type))
            {
                return BadRequest("Fee type is required");
            }

            if (fee.amount <= 0)
            {
                return BadRequest("Amount must be greater than 0");
            }

            var createdFee = await _feesRepository.CreateAsync(fee);
            return CreatedAtAction(nameof(GetAsync), new { id = createdFee.fee_id }, createdFee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing fee record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the fee record to update</param>
    /// <param name="fee">The fee object with updated information</param>
    /// <returns>The updated Fees object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Fees?>> UpdateAsync(int id, [FromBody] Fees fee)
    {
        try
        {
            // Validate required fields
            if (fee.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(fee.fee_type))
            {
                return BadRequest("Fee type is required");
            }

            if (fee.amount <= 0)
            {
                return BadRequest("Amount must be greater than 0");
            }

            var updatedFee = await _feesRepository.UpdateAsync(id, fee);
            if (updatedFee == null)
            {
                return NotFound($"Fee record with ID {id} not found");
            }
            return Ok(updatedFee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a fee record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the fee record to remove</param>
    /// <returns>The removed Fees object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Fees?>> RemoveAsync(int id)
    {
        try
        {
            var removedFee = await _feesRepository.RemoveAsync(id);
            if (removedFee == null)
            {
                return NotFound($"Fee record with ID {id} not found");
            }
            return Ok(removedFee);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}