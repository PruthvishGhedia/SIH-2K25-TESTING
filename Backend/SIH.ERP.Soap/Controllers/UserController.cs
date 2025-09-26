using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing users in the ERP system.
/// This controller provides CRUD operations for user accounts, authentication, and user-related information management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Retrieves a list of users with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of users to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of users to skip for pagination (default: 0)</param>
    /// <returns>A collection of User objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var users = await _userRepository.ListAsync(limit, offset);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user record</param>
    /// <returns>The User object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User?>> GetAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new user record in the system.
    /// </summary>
    /// <param name="user">The user object to create</param>
    /// <returns>The created User object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync([FromBody] User user)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(user.full_name))
            {
                return BadRequest("Full name is required");
            }

            if (string.IsNullOrWhiteSpace(user.email))
            {
                return BadRequest("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(user.email))
            {
                return BadRequest("Email format is invalid");
            }

            var createdUser = await _userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(GetAsync), new { id = createdUser.user_id }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing user record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update</param>
    /// <param name="user">The user object with updated information</param>
    /// <returns>The updated User object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<User?>> UpdateAsync(int id, [FromBody] User user)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(user.full_name))
            {
                return BadRequest("Full name is required");
            }

            if (string.IsNullOrWhiteSpace(user.email))
            {
                return BadRequest("Email is required");
            }

            // Validate email format
            if (!IsValidEmail(user.email))
            {
                return BadRequest("Email format is invalid");
            }

            var updatedUser = await _userRepository.UpdateAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found");
            }
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a user record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to remove</param>
    /// <returns>The removed User object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<User?>> RemoveAsync(int id)
    {
        try
        {
            var removedUser = await _userRepository.RemoveAsync(id);
            if (removedUser == null)
            {
                return NotFound($"User with ID {id} not found");
            }
            return Ok(removedUser);
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