namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a student payment transaction in the educational institution.
/// This model stores information about financial transactions, including amounts, dates, and payment status.
/// </summary>
public class Payment
{
    /// <summary>
    /// Unique identifier for the payment record.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int payment_id { get; set; }
    
    /// <summary>
    /// Identifier of the student making the payment.
    /// References the Student table to identify which student this payment belongs to.
    /// </summary>
    /// <example>1</example>
    public int student_id { get; set; }
    
    /// <summary>
    /// Amount of the payment in the institution's currency.
    /// Represents the monetary value of the transaction.
    /// </summary>
    /// <example>5000.00</example>
    public decimal amount { get; set; }
    
    /// <summary>
    /// Date when the payment was made.
    /// Used to track payment timelines and financial reporting.
    /// </summary>
    /// <example>2025-09-25</example>
    public string payment_date { get; set; } = string.Empty;
    
    /// <summary>
    /// Current status of the payment (e.g., Pending, Completed, Failed, Refunded).
    /// Used to track the current state of the financial transaction.
    /// </summary>
    /// <example>Completed</example>
    public string status { get; set; } = "pending";
    
    /// <summary>
    /// Mode of payment used (e.g., Online, Cash, Cheque, Bank Transfer).
    /// Used for accounting and payment method analysis.
    /// </summary>
    /// <example>Online</example>
    public string? mode { get; set; }
}