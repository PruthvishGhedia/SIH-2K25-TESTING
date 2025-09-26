using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing student admissions in the ERP system.
/// This controller provides CRUD operations for student admission applications and enrollment processes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AdmissionController : BaseController
{
    private readonly IAdmissionRepository _admissionRepository;

    public AdmissionController(IAdmissionRepository admissionRepository)
    {
        _admissionRepository = admissionRepository;
    }

    /// <summary>
    /// Retrieves a list of admission records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of admission records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of admission records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Admission objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Admission>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var admissions = await _admissionRepository.ListAsync(limit, offset);
            return Ok(admissions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific admission record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the admission record</param>
    /// <returns>The Admission object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Admission?>> GetAsync(int id)
    {
        try
        {
            var admission = await _admissionRepository.GetAsync(id);
            if (admission == null)
            {
                return NotFound($"Admission record with ID {id} not found");
            }
            return Ok(admission);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new admission record in the system.
    /// </summary>
    /// <param name="admission">The admission object to create</param>
    /// <returns>The created Admission object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Admission>> CreateAsync([FromBody] Admission admission)
    {
        try
        {
            // Validate required fields
            if (admission.course_id <= 0)
            {
                return BadRequest("Course ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(admission.full_name))
            {
                return BadRequest("Full name is required");
            }

            if (string.IsNullOrWhiteSpace(admission.email))
            {
                return BadRequest("Email is required");
            }

            var createdAdmission = await _admissionRepository.CreateAsync(admission);
            return CreatedAtAction(nameof(GetAsync), new { id = createdAdmission.admission_id }, createdAdmission);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing admission record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the admission record to update</param>
    /// <param name="admission">The admission object with updated information</param>
    /// <returns>The updated Admission object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Admission?>> UpdateAsync(int id, [FromBody] Admission admission)
    {
        try
        {
            // Validate required fields
            if (admission.course_id <= 0)
            {
                return BadRequest("Course ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(admission.full_name))
            {
                return BadRequest("Full name is required");
            }

            if (string.IsNullOrWhiteSpace(admission.email))
            {
                return BadRequest("Email is required");
            }

            var updatedAdmission = await _admissionRepository.UpdateAsync(id, admission);
            if (updatedAdmission == null)
            {
                return NotFound($"Admission record with ID {id} not found");
            }
            return Ok(updatedAdmission);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes an admission record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the admission record to remove</param>
    /// <returns>The removed Admission object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Admission?>> RemoveAsync(int id)
    {
        try
        {
            var removedAdmission = await _admissionRepository.RemoveAsync(id);
            if (removedAdmission == null)
            {
                return NotFound($"Admission record with ID {id} not found");
            }
            return Ok(removedAdmission);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}