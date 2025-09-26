using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing student enrollments in the ERP system.
/// This controller provides CRUD operations for student course enrollments and academic registration.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : BaseController
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentController(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    /// <summary>
    /// Retrieves a list of enrollment records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of enrollment records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of enrollment records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Enrollment objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enrollment>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var enrollments = await _enrollmentRepository.ListAsync(limit, offset);
            return Ok(enrollments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific enrollment record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment record</param>
    /// <returns>The Enrollment object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Enrollment?>> GetAsync(int id)
    {
        try
        {
            var enrollment = await _enrollmentRepository.GetAsync(id);
            if (enrollment == null)
            {
                return NotFound($"Enrollment record with ID {id} not found");
            }
            return Ok(enrollment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new enrollment record in the system.
    /// </summary>
    /// <param name="enrollment">The enrollment object to create</param>
    /// <returns>The created Enrollment object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Enrollment>> CreateAsync([FromBody] Enrollment enrollment)
    {
        try
        {
            // Validate required fields
            if (enrollment.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (enrollment.course_id <= 0)
            {
                return BadRequest("Course ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(enrollment.enrollment_date))
            {
                return BadRequest("Enrollment date is required");
            }

            var createdEnrollment = await _enrollmentRepository.CreateAsync(enrollment);
            return CreatedAtAction(nameof(GetAsync), new { id = createdEnrollment.enrollment_id }, createdEnrollment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing enrollment record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment record to update</param>
    /// <param name="enrollment">The enrollment object with updated information</param>
    /// <returns>The updated Enrollment object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Enrollment?>> UpdateAsync(int id, [FromBody] Enrollment enrollment)
    {
        try
        {
            // Validate required fields
            if (enrollment.student_id <= 0)
            {
                return BadRequest("Student ID is required and must be greater than 0");
            }

            if (enrollment.course_id <= 0)
            {
                return BadRequest("Course ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(enrollment.enrollment_date))
            {
                return BadRequest("Enrollment date is required");
            }

            var updatedEnrollment = await _enrollmentRepository.UpdateAsync(id, enrollment);
            if (updatedEnrollment == null)
            {
                return NotFound($"Enrollment record with ID {id} not found");
            }
            return Ok(updatedEnrollment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes an enrollment record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment record to remove</param>
    /// <returns>The removed Enrollment object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Enrollment?>> RemoveAsync(int id)
    {
        try
        {
            var removedEnrollment = await _enrollmentRepository.RemoveAsync(id);
            if (removedEnrollment == null)
            {
                return NotFound($"Enrollment record with ID {id} not found");
            }
            return Ok(removedEnrollment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}