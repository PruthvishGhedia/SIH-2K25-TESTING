using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing courses in the ERP system.
/// This controller provides CRUD operations for academic programs, including course details, department association, and fee structures.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CourseController : BaseController
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    /// <summary>
    /// Retrieves a list of courses with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of courses to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of courses to skip for pagination (default: 0)</param>
    /// <returns>A collection of Course objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var courses = await _courseRepository.ListAsync(limit, offset);
            return Ok(courses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific course by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the course record</param>
    /// <returns>The Course object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Course?>> GetAsync(int id)
    {
        try
        {
            var course = await _courseRepository.GetAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }
            return Ok(course);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new course record in the system.
    /// </summary>
    /// <param name="course">The course object to create</param>
    /// <returns>The created Course object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Course>> CreateAsync([FromBody] Course course)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(course.course_name))
            {
                return BadRequest("Course name is required");
            }

            if (string.IsNullOrWhiteSpace(course.course_code))
            {
                return BadRequest("Course code is required");
            }

            var createdCourse = await _courseRepository.CreateAsync(course);
            return CreatedAtAction(nameof(GetAsync), new { id = createdCourse.course_id }, createdCourse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing course record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the course to update</param>
    /// <param name="course">The course object with updated information</param>
    /// <returns>The updated Course object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Course?>> UpdateAsync(int id, [FromBody] Course course)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(course.course_name))
            {
                return BadRequest("Course name is required");
            }

            if (string.IsNullOrWhiteSpace(course.course_code))
            {
                return BadRequest("Course code is required");
            }

            var updatedCourse = await _courseRepository.UpdateAsync(id, course);
            if (updatedCourse == null)
            {
                return NotFound($"Course with ID {id} not found");
            }
            return Ok(updatedCourse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a course record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the course to remove</param>
    /// <returns>The removed Course object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Course?>> RemoveAsync(int id)
    {
        try
        {
            var removedCourse = await _courseRepository.RemoveAsync(id);
            if (removedCourse == null)
            {
                return NotFound($"Course with ID {id} not found");
            }
            return Ok(removedCourse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}