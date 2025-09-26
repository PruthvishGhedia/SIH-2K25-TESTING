using Microsoft.AspNetCore.Mvc;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for managing rooms in the ERP system.
/// This controller provides CRUD operations for hostel room information and occupancy management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomController : BaseController
{
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    /// <summary>
    /// Retrieves a list of room records with pagination support.
    /// </summary>
    /// <param name="limit">Maximum number of room records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of room records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Room objects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> ListAsync(
        [FromQuery] int limit = 100, 
        [FromQuery] int offset = 0)
    {
        try
        {
            var rooms = await _roomRepository.ListAsync(limit, offset);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific room record by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the room record</param>
    /// <returns>The Room object if found, null otherwise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Room?>> GetAsync(int id)
    {
        try
        {
            var room = await _roomRepository.GetAsync(id);
            if (room == null)
            {
                return NotFound($"Room record with ID {id} not found");
            }
            return Ok(room);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new room record in the system.
    /// </summary>
    /// <param name="room">The room object to create</param>
    /// <returns>The created Room object with assigned ID</returns>
    [HttpPost]
    public async Task<ActionResult<Room>> CreateAsync([FromBody] Room room)
    {
        try
        {
            // Validate required fields
            if (room.hostel_id <= 0)
            {
                return BadRequest("Hostel ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(room.room_no))
            {
                return BadRequest("Room number is required");
            }

            var createdRoom = await _roomRepository.CreateAsync(room);
            return CreatedAtAction(nameof(GetAsync), new { id = createdRoom.room_id }, createdRoom);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates an existing room record with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the room record to update</param>
    /// <param name="room">The room object with updated information</param>
    /// <returns>The updated Room object if successful, null otherwise</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Room?>> UpdateAsync(int id, [FromBody] Room room)
    {
        try
        {
            // Validate required fields
            if (room.hostel_id <= 0)
            {
                return BadRequest("Hostel ID is required and must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(room.room_no))
            {
                return BadRequest("Room number is required");
            }

            var updatedRoom = await _roomRepository.UpdateAsync(id, room);
            if (updatedRoom == null)
            {
                return NotFound($"Room record with ID {id} not found");
            }
            return Ok(updatedRoom);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a room record from the system by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the room record to remove</param>
    /// <returns>The removed Room object if successful, null otherwise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Room?>> RemoveAsync(int id)
    {
        try
        {
            var removedRoom = await _roomRepository.RemoveAsync(id);
            if (removedRoom == null)
            {
                return NotFound($"Room record with ID {id} not found");
            }
            return Ok(removedRoom);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}