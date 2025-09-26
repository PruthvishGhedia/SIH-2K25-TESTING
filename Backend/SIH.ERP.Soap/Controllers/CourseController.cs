using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing courses in the educational institution.
/// Provides full CRUD operations for course records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    /// <summary>
    /// Initializes a new instance of the CourseController class.
    /// </summary>
    /// <param name="courseRepository">The course repository for data access.</param>
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
    /// <response code="200">Returns the list of courses</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses(int limit = 100, int offset = 0)
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
    /// <returns>The Course object if found</returns>
    /// <response code="200">Returns the requested course</response>
    /// <response code="404">Course not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        try
        {
            var course = await _courseRepository.GetAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
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
    /// <response code="201">Returns the created course</response>
    /// <response code="400">Invalid course data provided</response>
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
    {
        try
        {
            if (course == null)
            {
                return BadRequest("Course data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(course.course_name))
            {
                ModelState.AddModelError("CourseName", "Course name is required.");
            }

            if (string.IsNullOrWhiteSpace(course.course_code))
            {
                ModelState.AddModelError("CourseCode", "Course code is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCourse = await _courseRepository.CreateAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.course_id }, createdCourse);
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
    /// <returns>The updated Course object if successful</returns>
    /// <response code="200">Returns the updated course</response>
    /// <response code="400">Invalid course data provided</response>
    /// <response code="404">Course not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Course>> UpdateCourse(int id, [FromBody] Course course)
    {
        try
        {
            if (course == null)
            {
                return BadRequest("Course data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(course.course_name))
            {
                ModelState.AddModelError("CourseName", "Course name is required.");
            }

            if (string.IsNullOrWhiteSpace(course.course_code))
            {
                ModelState.AddModelError("CourseCode", "Course code is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCourse = await _courseRepository.GetAsync(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            var updatedCourse = await _courseRepository.UpdateAsync(id, course);
            if (updatedCourse == null)
            {
                return NotFound($"Course with ID {id} could not be updated.");
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
    /// <returns>Success status if the course was removed</returns>
    /// <response code="204">Course successfully removed</response>
    /// <response code="404">Course not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        try
        {
            var course = await _courseRepository.GetAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            var removedCourse = await _courseRepository.RemoveAsync(id);
            if (removedCourse == null)
            {
                return NotFound($"Course with ID {id} could not be removed.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}