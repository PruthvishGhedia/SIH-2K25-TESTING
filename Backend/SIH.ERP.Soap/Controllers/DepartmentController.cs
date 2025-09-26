using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing departments in the ERP system.
/// This controller provides CRUD operations for academic and administrative departments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DepartmentController : BaseController
{
    private readonly IDepartmentRepository _departmentRepository;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
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
    /// <returns>The Department object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Department?>> GetAsync(int id)
    {
        try
        {
            var department = await _departmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound($"Department with ID {id} not found");
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
    [HttpPost]
    public async Task<ActionResult<Department>> CreateAsync([FromBody] Department department)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(department.dept_name))
            {
                return BadRequest("Department name is required");
            }

            var createdDepartment = await _departmentRepository.CreateAsync(department);
            return CreatedAtAction(nameof(GetAsync), new { id = createdDepartment.dept_id }, createdDepartment);
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
    /// <returns>The updated Department object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Department?>> UpdateAsync(int id, [FromBody] Department department)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(department.dept_name))
            {
                return BadRequest("Department name is required");
            }

            var updatedDepartment = await _departmentRepository.UpdateAsync(id, department);
            if (updatedDepartment == null)
            {
                return NotFound($"Department with ID {id} not found");
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
    /// <returns>The removed Department object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Department?>> RemoveAsync(int id)
    {
        try
        {
            var removedDepartment = await _departmentRepository.RemoveAsync(id);
            if (removedDepartment == null)
            {
                return NotFound($"Department with ID {id} not found");
            }
            return Ok(removedDepartment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}