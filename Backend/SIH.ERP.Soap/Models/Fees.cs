namespace SIH.ERP.Soap.Models;

public class Fees
{
    public int fee_id { get; set; }
    public int? student_id { get; set; }
    public string? fee_type { get; set; }
    public decimal? amount { get; set; }
    public DateTime? due_date { get; set; }
    public DateTime? paid_on { get; set; }
    public string? payment_status { get; set; }
    public string? payment_mode { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}