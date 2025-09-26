using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing student attendance in the ERP system.
/// This service handles student attendance records, including presence tracking and date information.
/// </summary>
[ServiceContract]
public interface IAttendanceService
{
    /// <summary>
    /// Retrieves a list of attendance records with pagination support.
    /// Use this endpoint to get multiple attendance records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of attendance records to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of attendance records to skip for pagination (default: 0)</param>
    /// <returns>A collection of Attendance objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 attendance records
    /// var attendances = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/attendances?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Attendance>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific attendance record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific student's attendance.
    /// </summary>
    /// <param name="attendance_id">The unique identifier of the attendance record (must be greater than 0)</param>
    /// <returns>The Attendance object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get attendance record with ID 1
    /// var attendance = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/attendances/{attendance_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Attendance?> GetAsync(string attendance_id);

    /// <summary>
    /// Creates a new attendance record in the system.
    /// Use this endpoint to record a student's attendance for a specific course on a specific date.
    /// </summary>
    /// <param name="item">The attendance object to create. The attendance_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Attendance object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new attendance record
    /// var newAttendance = new Attendance 
    /// {
    ///     student_id = 1,
    ///     course_id = 101,
    ///     date = "2025-09-25",
    ///     present = true
    /// };
    /// var createdAttendance = await CreateAsync(newAttendance);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/attendances", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Attendance> CreateAsync(Attendance item);

    /// <summary>
    /// Updates an existing attendance record with new information.
    /// Use this endpoint to modify an existing attendance record, such as correcting presence status.
    /// </summary>
    /// <param name="attendance_id">The unique identifier of the attendance record to update (must match the ID in the item parameter)</param>
    /// <param name="item">The attendance object with updated information</param>
    /// <returns>The updated Attendance object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update attendance record with ID 1
    /// var updatedAttendance = new Attendance 
    /// {
    ///     attendance_id = 1,
    ///     student_id = 1,
    ///     course_id = 101,
    ///     date = "2025-09-25",
    ///     present = false
    /// };
    /// var result = await UpdateAsync("1", updatedAttendance);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/attendances/{attendance_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Attendance?> UpdateAsync(string attendance_id, Attendance item);

    /// <summary>
    /// Removes an attendance record from the system by its unique identifier.
    /// Use this endpoint to delete an attendance record, typically used for data correction.
    /// </summary>
    /// <param name="attendance_id">The unique identifier of the attendance record to remove (must be greater than 0)</param>
    /// <returns>The removed Attendance object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove attendance record with ID 1
    /// var removedAttendance = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/attendances/{attendance_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Attendance?> RemoveAsync(string attendance_id);
}