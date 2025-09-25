using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing exams in the ERP system.
/// This service handles examination schedules, types, and related information for academic assessments.
/// </summary>
[ServiceContract]
public interface IExamService
{
    /// <summary>
    /// Retrieves a list of exams with pagination support.
    /// Use this endpoint to get multiple exam records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of exams to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of exams to skip for pagination (default: 0)</param>
    /// <returns>A collection of Exam objects</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/exams?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Exam>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific exam by its unique identifier.
    /// Use this endpoint to get detailed information about a specific examination.
    /// </summary>
    /// <param name="exam_id">The unique identifier of the exam record (must be greater than 0)</param>
    /// <returns>The Exam object if found, null otherwise</returns>
    [OperationContract]
    [WebGet(UriTemplate = "/exams/{exam_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Exam?> GetAsync(string exam_id);

    /// <summary>
    /// Creates a new exam record in the system.
    /// Use this endpoint to schedule a new examination for a subject.
    /// </summary>
    /// <param name="item">The exam object to create. The exam_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Exam object with assigned ID</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/exams", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Exam> CreateAsync(Exam item);

    /// <summary>
    /// Updates an existing exam record with new information.
    /// Use this endpoint to modify an existing exam, such as rescheduling or changing assessment type.
    /// </summary>
    /// <param name="exam_id">The unique identifier of the exam to update (must match the ID in the item parameter)</param>
    /// <param name="item">The exam object with updated information</param>
    /// <returns>The updated Exam object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/exams/{exam_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Exam?> UpdateAsync(string exam_id, Exam item);

    /// <summary>
    /// Removes an exam record from the system by its unique identifier.
    /// Use this endpoint to delete an exam record, typically used when exams are cancelled.
    /// </summary>
    /// <param name="exam_id">The unique identifier of the exam to remove (must be greater than 0)</param>
    /// <returns>The removed Exam object if successful, null otherwise</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/exams/{exam_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Exam?> RemoveAsync(string exam_id);
}