namespace SIH.ERP.Soap.Models;

public class BookIssue
{
    public int issue_id { get; set; }
    public int? book_id { get; set; }
    public int? student_id { get; set; }
    public DateTime? issue_date { get; set; }
    public DateTime? return_date { get; set; }
    public string? status { get; set; }
}