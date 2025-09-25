using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing exams in the educational institution.
/// Provides full CRUD operations for exam records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamRepository _examRepository;

    /// <summary>
    /// Initializes a new instance of the ExamController class.
    /// </summary>
    /// <param name="examRepository">The exam repository for data access.</param>
    public ExamController(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    /// <summary>
    /// Retrieves a list of exams with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of exams to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of exams to skip for pagination (default: 0)</param>
    /// <returns>A collection of Exam objects</returns>
    /// <response code="200">Returns the list of exams</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exam>>> GetExams(int limit = 100, int offset = 0)
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
    /// Retrieves a specific exam by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the exam record</param>
    /// <returns>The Exam object if found</returns>
    /// <response code="200">Returns the requested exam</response>
    /// <response code="404">Exam not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Exam>> GetExam(int id)
    {
        try
        {
            var exam = await _examRepository.GetAsync(id);
            if (exam == null)
            {
                return NotFound($"Exam with ID {id} not found.");
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
    /// <response code="201">Returns the created exam</response>
    /// <response code="400">Invalid exam data provided</response>
    [HttpPost]
    public async Task<ActionResult<Exam>> CreateExam([FromBody] Exam exam)
    {
        try
        {
            if (exam == null)
            {
                return BadRequest("Exam data is required.");
            }

            // Validate required fields
            if (!exam.exam_date.HasValue)
            {
                ModelState.AddModelError("ExamDate", "Exam date is required.");
            }

            if (exam.subject_code <= 0)
            {
                ModelState.AddModelError("SubjectCode", "Valid subject code is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdExam = await _examRepository.CreateAsync(exam);
            return CreatedAtAction(nameof(GetExam), new { id = createdExam.exam_id }, createdExam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing exam record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the exam to update</param>
    /// <param name="exam">The exam object with updated information</param>
    /// <returns>The updated Exam object if successful</returns>
    /// <response code="200">Returns the updated exam</response>
    /// <response code="400">Invalid exam data provided</response>
    /// <response code="404">Exam not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Exam>> UpdateExam(int id, [FromBody] Exam exam)
    {
        try
        {
            if (exam == null)
            {
                return BadRequest("Exam data is required.");
            }

            // Validate required fields
            if (!exam.exam_date.HasValue)
            {
                ModelState.AddModelError("ExamDate", "Exam date is required.");
            }

            if (exam.subject_code <= 0)
            {
                ModelState.AddModelError("SubjectCode", "Valid subject code is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingExam = await _examRepository.GetAsync(id);
            if (existingExam == null)
            {
                return NotFound($"Exam with ID {id} not found.");
            }

            var updatedExam = await _examRepository.UpdateAsync(id, exam);
            if (updatedExam == null)
            {
                return NotFound($"Exam with ID {id} could not be updated.");
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
    /// <param name="id">The unique identifier of the exam to remove</param>
    /// <returns>Success status if the exam was removed</returns>
    /// <response code="204">Exam successfully removed</response>
    /// <response code="404">Exam not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExam(int id)
    {
        try
        {
            var exam = await _examRepository.GetAsync(id);
            if (exam == null)
            {
                return NotFound($"Exam with ID {id} not found.");
            }

            var removedExam = await _examRepository.RemoveAsync(id);
            if (removedExam == null)
            {
                return NotFound($"Exam with ID {id} could not be removed.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}