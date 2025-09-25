namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student fee record in the educational institution.
/// This model stores information about fees associated with students, including payment status and details.
/// </summary>
public class Fees
{
    /// <summary>
    /// Unique identifier for the fee record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int fee_id { get; set; }
    
    /// <summary>
    /// Identifier of the student associated with this fee record.
    /// References the Student table to identify the student responsible for the fee.
    /// </summary>
    /// <example>1</example>
    public int? student_id { get; set; }
    
    /// <summary>
    /// Type of fee (e.g., Tuition, Hostel, Library, Examination).
    /// Used to categorize fees for reporting and accounting purposes.
    /// </summary>
    /// <example>Tuition</example>
    public string? fee_type { get; set; }
    
    /// <summary>
    /// Amount of the fee in the institution's currency.
    /// Represents the monetary value that needs to be paid.
    /// </summary>
    /// <example>5000.00</example>
    public decimal? amount { get; set; }
    
    /// <summary>
    /// Due date for the fee payment.
    /// Used to track payment deadlines and send reminders.
    /// </summary>
    /// <example>2025-12-31T00:00:00</example>
    public DateTime? due_date { get; set; }
    
    /// <summary>
    /// Date when the fee was paid.
    /// Used to track payment history and update payment status.
    /// </summary>
    /// <example>2025-09-25T10:30:00</example>
    public DateTime? paid_on { get; set; }
    
    /// <summary>
    /// Current status of the fee payment (e.g., Pending, Paid, Overdue).
    /// Used to track payment progress and identify outstanding fees.
    /// </summary>
    /// <example>Paid</example>
    public string? payment_status { get; set; }
    
    /// <summary>
    /// Mode of payment used (e.g., Online, Cash, Cheque, Bank Transfer).
    /// Used for accounting and payment method analysis.
    /// </summary>
    /// <example>Online</example>
    public string? payment_mode { get; set; }
    
    /// <summary>
    /// Date and time when the fee record was created.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T09:00:00</example>
    public DateTime? created_at { get; set; }
    
    /// <summary>
    /// Date and time when the fee record was last updated.
    /// System-generated timestamp for audit and tracking purposes.
    /// </summary>
    /// <example>2025-09-25T14:45:00</example>
    public DateTime? updated_at { get; set; }
}