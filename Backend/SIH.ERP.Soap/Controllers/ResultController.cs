using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing student results in the ERP system.
/// This controller provides CRUD operations for examination results and academic performance tracking.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ResultController : BaseController
{
    private readonly IResultRepository _resultRepository;

    public ResultController(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    /// <summary>
    /// Retrieves a list of result records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of result records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of result records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Result objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Result>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var results = await _resultRepository.ListAsync(limit, offset);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific result record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the result record</param>
    /// <returns>The Result object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result?>> GetAsync(int id)
    {
        try
        {
            var result = await _resultRepository.GetAsync(id);
            if (result == null)
            {
                return NotFound($"Result record with ID {id} not found");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new result record in the system.
    /// </summary>
    /// <param name="result">The result object to create</param>
    /// <returns>The created Result object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Result>> CreateAsync([FromBody] Result result)
    {
        try
        {
            // Validate required fields
            if (result.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (result.exam_id <= 0)
            {
                return BadRequest("Exam ID is required and must be greater than 0");
            }

            if (result.marks < 0)
            {
                return BadRequest("Marks must be greater than or equal to 0");
            }

            var createdResult = await _resultRepository.CreateAsync(result);
            return CreatedAtAction(nameof(GetAsync), new { id = createdResult.result_id }, createdResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing result record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the result record to update</param>
    /// <param name="result">The result object with updated information</param>
    /// <returns>The updated Result object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Result?>> UpdateAsync(int id, [FromBody] Result result)
    {
        try
        {
            // Validate required fields
            if (result.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (result.exam_id <= 0)
            {
                return BadRequest("Exam ID is required and must be greater than 0");
            }

            if (result.marks < 0)
            {
                return BadRequest("Marks must be greater than or equal to 0");
            }

            var updatedResult = await _resultRepository.UpdateAsync(id, result);
            if (updatedResult == null)
            {
                return NotFound($"Result record with ID {id} not found");
            }
            return Ok(updatedResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a result record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the result record to remove</param>
    /// <returns>The removed Result object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result?>> RemoveAsync(int id)
    {
        try
        {
            var removedResult = await _resultRepository.RemoveAsync(id);
            if (removedResult == null)
            {
                return NotFound($"Result record with ID {id} not found");
            }
            return Ok(removedResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}