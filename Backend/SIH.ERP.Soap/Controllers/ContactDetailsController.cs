using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing contact details in the ERP system.
/// This controller provides CRUD operations for user contact information including phone numbers, emails, and addresses.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContactDetailsController : BaseController
{
    private readonly IContactDetailsRepository _contactDetailsRepository;

    public ContactDetailsController(IContactDetailsRepository contactDetailsRepository)
    {
        _contactDetailsRepository = contactDetailsRepository;
    }

    /// <summary>
    /// Retrieves a list of contact details records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of contact details records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of contact details records to skip for pagination (default: 0)</param>
    /// <returns>A collection of ContactDetails objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactDetails>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var contacts = await _contactDetailsRepository.ListAsync(limit, offset);
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific contact details record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the contact details record</param>
    /// <returns>The ContactDetails object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactDetails?>> GetAsync(int id)
    {
        try
        {
            var contact = await _contactDetailsRepository.GetAsync(id);
            if (contact == null)
            {
                return NotFound($"Contact details record with ID {id} not found");
            }
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new contact details record in the system.
    /// </summary>
    /// <param name="contact">The contact details object to create</param>
    /// <returns>The created ContactDetails object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<ContactDetails>> CreateAsync([FromBody] ContactDetails contact)
    {
        try
        {
            // Validate required fields
            if (contact.user_id <= 0)
            {
                return BadRequest("User ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(contact.contact_type))
            {
                return BadRequest("Contact type is required");
            }

            if (string.IsNullOrWhiteSpace(contact.contact_value))
            {
                return BadRequest("Contact value is required");
            }

            var createdContact = await _contactDetailsRepository.CreateAsync(contact);
            return CreatedAtAction(nameof(GetAsync), new { id = createdContact.contact_id }, createdContact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing contact details record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the contact details record to update</param>
    /// <param name="contact">The contact details object with updated information</param>
    /// <returns>The updated ContactDetails object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ContactDetails?>> UpdateAsync(int id, [FromBody] ContactDetails contact)
    {
        try
        {
            // Validate required fields
            if (contact.user_id <= 0)
            {
                return BadRequest("User ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(contact.contact_type))
            {
                return BadRequest("Contact type is required");
            }

            if (string.IsNullOrWhiteSpace(contact.contact_value))
            {
                return BadRequest("Contact value is required");
            }

            var updatedContact = await _contactDetailsRepository.UpdateAsync(id, contact);
            if (updatedContact == null)
            {
                return NotFound($"Contact details record with ID {id} not found");
            }
            return Ok(updatedContact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a contact details record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the contact details record to remove</param>
    /// <returns>The removed ContactDetails object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ContactDetails?>> RemoveAsync(int id)
    {
        try
        {
            var removedContact = await _contactDetailsRepository.RemoveAsync(id);
            if (removedContact == null)
            {
                return NotFound($"Contact details record with ID {id} not found");
            }
            return Ok(removedContact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}