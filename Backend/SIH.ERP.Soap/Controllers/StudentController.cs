using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing students in the ERP system.
/// This controller provides CRUD operations for student records, including personal information, academic details, and enrollment data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StudentController : BaseController
{
    private readonly IStudentRepository _studentRepository;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
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
    /// <returns>The Student object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Student?>> GetAsync(int id)
    {
        try
        {
            var student = await _studentRepository.GetAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found");
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
    [HttpPost]
    public async Task<ActionResult<Student>> CreateAsync([FromBody] Student student)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(student.first_name))
            {
                return BadRequest("First name is required");
            }

            if (string.IsNullOrWhiteSpace(student.last_name))
            {
                return BadRequest("Last name is required");
            }

            if (string.IsNullOrWhiteSpace(student.email))
            {
                return BadRequest("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(student.email))
            {
                return BadRequest("Email format is invalid");
            }

            var createdStudent = await _studentRepository.CreateAsync(student);
            return CreatedAtAction(nameof(GetAsync), new { id = createdStudent.student_id }, createdStudent);
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
    /// <returns>The updated Student object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Student?>> UpdateAsync(int id, [FromBody] Student student)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(student.first_name))
            {
                return BadRequest("First name is required");
            }

            if (string.IsNullOrWhiteSpace(student.last_name))
            {
                return BadRequest("Last name is required");
            }

            if (string.IsNullOrWhiteSpace(student.email))
            {
                return BadRequest("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(student.email))
            {
                return BadRequest("Email format is invalid");
            }

            var updatedStudent = await _studentRepository.UpdateAsync(id, student);
            if (updatedStudent == null)
            {
                return NotFound($"Student with ID {id} not found");
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
    /// <returns>The removed Student object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Student?>> RemoveAsync(int id)
    {
        try
        {
            var removedStudent = await _studentRepository.RemoveAsync(id);
            if (removedStudent == null)
            {
                return NotFound($"Student with ID {id} not found");
            }
            return Ok(removedStudent);
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