using CoreWCF;
using CoreWCF.Web;
using SIH.ERP.Soap.Models;

namespace SIH.ERP.Soap.Contracts;

/// <summary>
/// Service contract for managing student payments in the ERP system.
/// This service handles financial transactions, including payment amounts, dates, and status tracking.
/// </summary>
[ServiceContract]
public interface IPaymentService
{
    /// <summary>
    /// Retrieves a list of payment records with pagination support.
    /// Use this endpoint to get multiple payment records with optional pagination.
    /// </summary>
    /// <param name="limit">Maximum number of payments to retrieve (default: 100, maximum: 1000)</param>
    /// <param name="offset">Number of payments to skip for pagination (default: 0)</param>
    /// <returns>A collection of Payment objects</returns>
    /// <example>
    /// <code>
    /// // Get first 10 payments
    /// var payments = await ListAsync(10, 0);
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/payments?limit={limit}&offset={offset}", ResponseFormat = WebMessageFormat.Json)]
    Task<IEnumerable<Payment>> ListAsync(int limit = 100, int offset = 0);

    /// <summary>
    /// Retrieves a specific payment record by its unique identifier.
    /// Use this endpoint to get detailed information about a specific financial transaction.
    /// </summary>
    /// <param name="payment_id">The unique identifier of the payment record (must be greater than 0)</param>
    /// <returns>The Payment object if found, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Get payment record with ID 1
    /// var payment = await GetAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebGet(UriTemplate = "/payments/{payment_id}", ResponseFormat = WebMessageFormat.Json)]
    Task<Payment?> GetAsync(string payment_id);

    /// <summary>
    /// Creates a new payment record in the system.
    /// Use this endpoint to record a student's payment transaction.
    /// </summary>
    /// <param name="item">The payment object to create. The payment_id field is ignored and will be assigned automatically.</param>
    /// <returns>The created Payment object with assigned ID</returns>
    /// <example>
    /// <code>
    /// // Create a new payment record
    /// var newPayment = new Payment 
    /// {
    ///     student_id = 1,
    ///     amount = 5000.00m,
    ///     payment_date = "2025-09-25",
    ///     status = "Completed",
    ///     mode = "Online"
    /// };
    /// var createdPayment = await CreateAsync(newPayment);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/payments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Payment> CreateAsync(Payment item);

    /// <summary>
    /// Updates an existing payment record with new information.
    /// Use this endpoint to modify an existing payment record, such as updating status or payment mode.
    /// </summary>
    /// <param name="payment_id">The unique identifier of the payment record to update (must match the ID in the item parameter)</param>
    /// <param name="item">The payment object with updated information</param>
    /// <returns>The updated Payment object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Update payment record with ID 1
    /// var updatedPayment = new Payment 
    /// {
    ///     payment_id = 1,
    ///     student_id = 1,
    ///     amount = 5000.00m,
    ///     payment_date = "2025-09-25",
    ///     status = "Refunded",
    ///     mode = "Online"
    /// };
    /// var result = await UpdateAsync("1", updatedPayment);
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/payments/{payment_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Payment?> UpdateAsync(string payment_id, Payment item);

    /// <summary>
    /// Removes a payment record from the system by its unique identifier.
    /// Use this endpoint to delete a payment record, typically used for data correction.
    /// </summary>
    /// <param name="payment_id">The unique identifier of the payment record to remove (must be greater than 0)</param>
    /// <returns>The removed Payment object if successful, null otherwise</returns>
    /// <example>
    /// <code>
    /// // Remove payment record with ID 1
    /// var removedPayment = await RemoveAsync("1");
    /// </code>
    /// </example>
    [OperationContract]
    [WebInvoke(Method = "DELETE", UriTemplate = "/payments/{payment_id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    Task<Payment?> RemoveAsync(string payment_id);
}