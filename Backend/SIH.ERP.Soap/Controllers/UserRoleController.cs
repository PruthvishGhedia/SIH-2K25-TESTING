using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing user-role assignments in the ERP system.
/// This controller provides CRUD operations for mapping users to their assigned roles for access control.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserRoleController : BaseController
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleController(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    /// <summary>
    /// Retrieves a list of user-role assignment records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of user-role assignment records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of user-role assignment records to skip for pagination (default: 0)</param>
    /// <returns>A collection of UserRole objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRole>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var userRoles = await _userRoleRepository.ListAsync(limit, offset);
            return Ok(userRoles);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific user-role assignment record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user-role assignment record</param>
    /// <returns>The UserRole object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserRole?>> GetAsync(int id)
    {
        try
        {
            var userRole = await _userRoleRepository.GetAsync(id);
            if (userRole == null)
            {
                return NotFound($"User-role assignment record with ID {id} not found");
            }
            return Ok(userRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new user-role assignment record in the system.
    /// </summary>
    /// <param name="userRole">The user-role assignment object to create</param>
    /// <returns>The created UserRole object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<UserRole>> CreateAsync([FromBody] UserRole userRole)
    {
        try
        {
            // Validate required fields
            if (userRole.user_id <= 0)
            {
                return BadRequest("User ID is required and must be greater than 0");
            }

            if (userRole.role_id <= 0)
            {
                return BadRequest("Role ID is required and must be greater than 0");
            }

            var createdUserRole = await _userRoleRepository.CreateAsync(userRole);
            return CreatedAtAction(nameof(GetAsync), new { id = createdUserRole.user_role_id }, createdUserRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing user-role assignment record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the user-role assignment record to update</param>
    /// <param name="userRole">The user-role assignment object with updated information</param>
    /// <returns>The updated UserRole object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserRole?>> UpdateAsync(int id, [FromBody] UserRole userRole)
    {
        try
        {
            // Validate required fields
            if (userRole.user_id <= 0)
            {
                return BadRequest("User ID is required and must be greater than 0");
            }

            if (userRole.role_id <= 0)
            {
                return BadRequest("Role ID is required and must be greater than 0");
            }

            var updatedUserRole = await _userRoleRepository.UpdateAsync(id, userRole);
            if (updatedUserRole == null)
            {
                return NotFound($"User-role assignment record with ID {id} not found");
            }
            return Ok(updatedUserRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a user-role assignment record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user-role assignment record to remove</param>
    /// <returns>The removed UserRole object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserRole?>> RemoveAsync(int id)
    {
        try
        {
            var removedUserRole = await _userRoleRepository.RemoveAsync(id);
            if (removedUserRole == null)
            {
                return NotFound($"User-role assignment record with ID {id} not found");
            }
            return Ok(removedUserRole);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}