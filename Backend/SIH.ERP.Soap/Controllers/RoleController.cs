using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing user roles in the ERP system.
/// This controller provides CRUD operations for system roles and access control management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoleController : BaseController
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Retrieves a list of role records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of role records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of role records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Role objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var roles = await _roleRepository.ListAsync(limit, offset);
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific role record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role record</param>
    /// <returns>The Role object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Role?>> GetAsync(int id)
    {
        try
        {
            var role = await _roleRepository.GetAsync(id);
            if (role == null)
            {
                return NotFound($"Role record with ID {id} not found");
            }
            return Ok(role);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new role record in the system.
    /// </summary>
    /// <param name="role">The role object to create</param>
    /// <returns>The created Role object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Role>> CreateAsync([FromBody] Role role)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(role.role_name))
            {
                return BadRequest("Role name is required");
            }

            var createdRole = await _roleRepository.CreateAsync(role);
            return CreatedAtAction(nameof(GetAsync), new { id = createdRole.role_id }, createdRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing role record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the role record to update</param>
    /// <param name="role">The role object with updated information</param>
    /// <returns>The updated Role object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Role?>> UpdateAsync(int id, [FromBody] Role role)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(role.role_name))
            {
                return BadRequest("Role name is required");
            }

            var updatedRole = await _roleRepository.UpdateAsync(id, role);
            if (updatedRole == null)
            {
                return NotFound($"Role record with ID {id} not found");
            }
            return Ok(updatedRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a role record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role record to remove</param>
    /// <returns>The removed Role object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Role?>> RemoveAsync(int id)
    {
        try
        {
            var removedRole = await _roleRepository.RemoveAsync(id);
            if (removedRole == null)
            {
                return NotFound($"Role record with ID {id} not found");
            }
            return Ok(removedRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}