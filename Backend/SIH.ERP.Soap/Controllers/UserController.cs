using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing users in the educational institution.
/// Provides full CRUD operations for user records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the UserController class.
    /// </summary>
    /// <param name="userRepository">The user repository for data access.</param>
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
    /// <response code="200">Returns the list of users</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(int limit = 100, int offset = 0)
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
    /// <returns>The User object if found</returns>
    /// <response code="200">Returns the requested user</response>
    /// <response code="404">User not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        try
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
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
    /// <response code="201">Returns the created user</response>
    /// <response code="400">Invalid user data provided</response>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(user.full_name))
            {
                ModelState.AddModelError("FullName", "Full name is required.");
            }

            if (string.IsNullOrWhiteSpace(user.email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (!IsValidEmail(user.email))
            {
                ModelState.AddModelError("Email", "Email format is invalid.");
            }

            if (string.IsNullOrWhiteSpace(user.password_hash))
            {
                ModelState.AddModelError("PasswordHash", "Password hash is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.user_id }, createdUser);
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
    /// <returns>The updated User object if successful</returns>
    /// <response code="200">Returns the updated user</response>
    /// <response code="400">Invalid user data provided</response>
    /// <response code="404">User not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            // Validate required fields
            if (string.IsNullOrWhiteSpace(user.full_name))
            {
                ModelState.AddModelError("FullName", "Full name is required.");
            }

            if (string.IsNullOrWhiteSpace(user.email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (!IsValidEmail(user.email))
            {
                ModelState.AddModelError("Email", "Email format is invalid.");
            }

            if (string.IsNullOrWhiteSpace(user.password_hash))
            {
                ModelState.AddModelError("PasswordHash", "Password hash is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetAsync(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var updatedUser = await _userRepository.UpdateAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} could not be updated.");
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
    /// <returns>Success status if the user was removed</returns>
    /// <response code="204">User successfully removed</response>
    /// <response code="404">User not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var removedUser = await _userRepository.RemoveAsync(id);
            if (removedUser == null)
            {
                return NotFound($"User with ID {id} could not be removed.");
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