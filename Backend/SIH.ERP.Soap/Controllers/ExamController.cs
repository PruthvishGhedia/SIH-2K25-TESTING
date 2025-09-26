using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing exams in the ERP system.
/// This controller provides CRUD operations for examination scheduling, assessment types, and exam tracking.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExamController : BaseController
{
    private readonly IExamRepository _examRepository;

    public ExamController(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    /// <summary>
    /// Retrieves a list of exam records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of exam records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of exam records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Exam objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exam>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var exams = await _examRepository.ListAsync(limit, offset);
            return Ok(exams);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific exam record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the exam record</param>
    /// <returns>The Exam object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Exam?>> GetAsync(int id)
    {
        try
        {
            var exam = await _examRepository.GetAsync(id);
            if (exam == null)
            {
                return NotFound($"Exam record with ID {id} not found");
            }
            return Ok(exam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new exam record in the system.
    /// </summary>
    /// <param name="exam">The exam object to create</param>
    /// <returns>The created Exam object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Exam>> CreateAsync([FromBody] Exam exam)
    {
        try
        {
            // Validate required fields
            if (exam.dept_id <= 0)
            {
                return BadRequest("Department ID is required and must be greater than 0");
            }

            if (exam.subject_code <= 0)
            {
                return BadRequest("Subject code is required and must be greater than 0");
            }

            if (exam.exam_date == null || exam.exam_date == default(DateTime))
            {
                return BadRequest("Exam date is required");
            }

            if (string.IsNullOrWhiteSpace(exam.assessment_type))
            {
                return BadRequest("Assessment type is required");
            }

            var createdExam = await _examRepository.CreateAsync(exam);
            return CreatedAtAction(nameof(GetAsync), new { id = createdExam.exam_id }, createdExam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing exam record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the exam record to update</param>
    /// <param name="exam">The exam object with updated information</param>
    /// <returns>The updated Exam object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Exam?>> UpdateAsync(int id, [FromBody] Exam exam)
    {
        try
        {
            // Validate required fields
            if (exam.dept_id <= 0)
            {
                return BadRequest("Department ID is required and must be greater than 0");
            }

            if (exam.subject_code <= 0)
            {
                return BadRequest("Subject code is required and must be greater than 0");
            }

            if (exam.exam_date == null || exam.exam_date == default(DateTime))
            {
                return BadRequest("Exam date is required");
            }

            if (string.IsNullOrWhiteSpace(exam.assessment_type))
            {
                return BadRequest("Assessment type is required");
            }

            var updatedExam = await _examRepository.UpdateAsync(id, exam);
            if (updatedExam == null)
            {
                return NotFound($"Exam record with ID {id} not found");
            }
            return Ok(updatedExam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes an exam record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the exam record to remove</param>
    /// <returns>The removed Exam object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Exam?>> RemoveAsync(int id)
    {
        try
        {
            var removedExam = await _examRepository.RemoveAsync(id);
            if (removedExam == null)
            {
                return NotFound($"Exam record with ID {id} not found");
            }
            return Ok(removedExam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}