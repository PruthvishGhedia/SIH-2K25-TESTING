using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing students in the educational institution.
/// Provides full CRUD operations for student records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    /// <summary>
    /// Initializes a new instance of the StudentController class.
    /// </summary>
    /// <param name="studentRepository">The student repository for data access.</param>
    public StudentController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    /// <summary>
    /// Retrieves a list of students with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of students to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of students to skip for pagination (default: 0)</param>
    /// <returns>A collection of Student objects</returns>
    /// <response code="200">Returns the list of students</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents(int limit = 100, int offset = 0)
    {
        try
        {
            var students = await _studentRepository.ListAsync(limit, offset);
            return Ok(students);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific student by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student record</param>
    /// <returns>The Student object if found</returns>
    /// <response code="200">Returns the requested student</response>
    /// <response code="404">Student not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        try
        {
            var student = await _studentRepository.GetAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new student record in the system.
    /// </summary>
    /// <param name="student">The student object to create</param>
    /// <returns>The created Student object with assigned ID</returns>
    /// <response code="201">Returns the created student</response>
    /// <response code="400">Invalid student data provided</response>
    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
    {
        try
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(student.first_name))
            {
                ModelState.AddModelError("FirstName", "First name is required.");
            }

            if (string.IsNullOrWhiteSpace(student.last_name))
            {
                ModelState.AddModelError("LastName", "Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(student.email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (!IsValidEmail(student.email))
            {
                ModelState.AddModelError("Email", "Email format is invalid.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStudent = await _studentRepository.CreateAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.student_id }, createdStudent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing student record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the student to update</param>
    /// <param name="student">The student object with updated information</param>
    /// <returns>The updated Student object if successful</returns>
    /// <response code="200">Returns the updated student</response>
    /// <response code="400">Invalid student data provided</response>
    /// <response code="404">Student not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
    {
        try
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(student.first_name))
            {
                ModelState.AddModelError("FirstName", "First name is required.");
            }

            if (string.IsNullOrWhiteSpace(student.last_name))
            {
                ModelState.AddModelError("LastName", "Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(student.email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (!IsValidEmail(student.email))
            {
                ModelState.AddModelError("Email", "Email format is invalid.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = await _studentRepository.GetAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            var updatedStudent = await _studentRepository.UpdateAsync(id, student);
            if (updatedStudent == null)
            {
                return NotFound($"Student with ID {id} could not be updated.");
            }

            return Ok(updatedStudent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a student record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to remove</param>
    /// <returns>Success status if the student was removed</returns>
    /// <response code="204">Student successfully removed</response>
    /// <response code="404">Student not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            var student = await _studentRepository.GetAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            var removedStudent = await _studentRepository.RemoveAsync(id);
            if (removedStudent == null)
            {
                return NotFound($"Student with ID {id} could not be removed.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}