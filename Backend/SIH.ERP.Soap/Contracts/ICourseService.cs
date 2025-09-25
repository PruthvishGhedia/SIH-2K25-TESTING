using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing courses in the ERP system.
/// This service handles academic programs, course offerings, and curriculum management.
/// </summary>
[ServiceContract]
public interface ICourseService
{
    /// <summary>
    /// Retrieves a list of courses with pagination support.
    /// Use this endpoint to get multiple course records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of courses to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of courses to skip for pagination (default: 0)</param>
    /// <returns>A collection of Course objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/courses?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Course>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific course by its unique identifier.
    /// Use this endpoint to get detailed information about a specific course offering.
    /// </summary>
    /// <param name="course_id">The unique identifier of the course record (must be greater than 0)</param>
    /// <returns>The Course object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/courses/{course_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Course?> GetAsync(string course_id);

    /// <summary>
    /// Creates a new course record in the system.
    /// Use this endpoint to add a new academic program or course offering to the institution.
    /// </summary>
    /// <param name="item">The course object to create. The course_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Course object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/courses", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Course> CreateAsync(Course item);

    /// <summary>
    /// Updates an existing course record with new information.
    /// Use this endpoint to modify an existing course offering, such as updating fees or description.
    /// </summary>
    /// <param name="course_id">The unique identifier of the course to update (must match the ID in the item parameter)</param>
    /// <param name="item">The course object with updated information</param>
    /// <returns>The updated Course object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/courses/{course_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Course?> UpdateAsync(string course_id, Course item);

    /// <summary>
    /// Removes a course record from the system by its unique identifier.
    /// Use this endpoint to delete a course offering, typically used when programs are discontinued.
    /// </summary>
    /// <param name="course_id">The unique identifier of the course to remove (must be greater than 0)</param>
    /// <returns>The removed Course object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/courses/{course_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Course?> RemoveAsync(string course_id);
}