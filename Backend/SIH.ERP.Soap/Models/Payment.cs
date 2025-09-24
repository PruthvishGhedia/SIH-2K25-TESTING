namespace SIH.ERP.Soap.Models;

public class Payment
{
    public int payment_id { get; set; }
    public int student_id { get; set; }
    public decimal amount { get; set; }
    public string payment_date { get; set; } = string.Empty;
    public string status { get; set; } = "pending";
    public string? mode { get; set; }
}

