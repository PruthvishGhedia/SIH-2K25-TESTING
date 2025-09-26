using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing departments in the educational institution.
/// Provides full CRUD operations for department records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// Initializes a new instance of the DepartmentController class.
    /// </summary>
    /// <param name="departmentRepository">The department repository for data access.</param>
    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// Retrieves a list of departments with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of departments to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of departments to skip for pagination (default: 0)</param>
    /// <returns>A collection of Department objects</returns>
    /// <response code="200">Returns the list of departments</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments(int limit = 100, int offset = 0)
    {
        try
        {
            var departments = await _departmentRepository.ListAsync(limit, offset);
            return Ok(departments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific department by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department record</param>
    /// <returns>The Department object if found</returns>
    /// <response code="200">Returns the requested department</response>
    /// <response code="404">Department not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        try
        {
            var department = await _departmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            return Ok(department);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new department record in the system.
    /// </summary>
    /// <param name="department">The department object to create</param>
    /// <returns>The created Department object with assigned ID</returns>
    /// <response code="201">Returns the created department</response>
    /// <response code="400">Invalid department data provided</response>
    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department department)
    {
        try
        {
            if (department == null)
            {
                return BadRequest("Department data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(department.dept_name))
            {
                ModelState.AddModelError("DeptName", "Department name is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDepartment = await _departmentRepository.CreateAsync(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.dept_id }, createdDepartment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing department record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the department to update</param>
    /// <param name="department">The department object with updated information</param>
    /// <returns>The updated Department object if successful</returns>
    /// <response code="200">Returns the updated department</response>
    /// <response code="400">Invalid department data provided</response>
    /// <response code="404">Department not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Department>> UpdateDepartment(int id, [FromBody] Department department)
    {
        try
        {
            if (department == null)
            {
                return BadRequest("Department data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(department.dept_name))
            {
                ModelState.AddModelError("DeptName", "Department name is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDepartment = await _departmentRepository.GetAsync(id);
            if (existingDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            var updatedDepartment = await _departmentRepository.UpdateAsync(id, department);
            if (updatedDepartment == null)
            {
                return NotFound($"Department with ID {id} could not be updated.");
            }

            return Ok(updatedDepartment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a department record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the department to remove</param>
    /// <returns>Success status if the department was removed</returns>
    /// <response code="204">Department successfully removed</response>
    /// <response code="404">Department not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        try
        {
            var department = await _departmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            var removedDepartment = await _departmentRepository.RemoveAsync(id);
            if (removedDepartment == null)
            {
                return NotFound($"Department with ID {id} could not be removed.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}