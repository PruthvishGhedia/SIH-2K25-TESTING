using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing rooms in the ERP system.
/// This service handles hostel room information, including capacity, occupancy status, and room assignments.
/// </summary>
[ServiceContract]
public interface IRoomService
{
    /// <summary>
    /// Retrieves a list of rooms with pagination support.
    /// Use this endpoint to get multiple room records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of rooms to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of rooms to skip for pagination (default: 0)</param>
    /// <returns>A collection of Room objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 rooms
    /// var rooms = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/rooms?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Room>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific room by its unique identifier.
    /// Use this endpoint to get detailed information about a specific room.
    /// </summary>
    /// <param name="room_id">The unique identifier of the room record (must be greater than 0)</param>
    /// <returns>The Room object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get room with ID 1
    /// var room = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/rooms/{room_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Room?> GetAsync(string room_id);

    /// <summary>
    /// Creates a new room record in the system.
    /// Use this endpoint to add a new room to a hostel facility.
    /// </summary>
    /// <param name="item">The room object to create. The room_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Room object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new room
    /// var newRoom = new Room 
    /// {
    ///     hostel_id = 1,
    ///     room_no = "A101",
    ///     capacity = 3,
    ///     occupancy_status = "Available"
    /// };
    /// var createdRoom = await CreateAsync(newRoom);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/rooms", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Room> CreateAsync(Room item);

    /// <summary>
    /// Updates an existing room record with new information.
    /// Use this endpoint to modify an existing room record, such as updating occupancy status or capacity.
    /// </summary>
    /// <param name="room_id">The unique identifier of the room to update (must match the ID in the item parameter)</param>
    /// <param name="item">The room object with updated information</param>
    /// <returns>The updated Room object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update room with ID 1
    /// var updatedRoom = new Room 
    /// {
    ///     room_id = 1,
    ///     hostel_id = 1,
    ///     room_no = "A101",
    ///     capacity = 3,
    ///     occupancy_status = "Occupied"
    /// };
    /// var result = await UpdateAsync("1", updatedRoom);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/rooms/{room_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Room?> UpdateAsync(string room_id, Room item);

    /// <summary>
    /// Removes a room record from the system by its unique identifier.
    /// Use this endpoint to delete a room record, typically used when facilities are restructured.
    /// </summary>
    /// <param name="room_id">The unique identifier of the room to remove (must be greater than 0)</param>
    /// <returns>The removed Room object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove room with ID 1
    /// var removedRoom = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/rooms/{room_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Room?> RemoveAsync(string room_id);
}