namespace SIH.ERP.Soap.Models;

public class Result
{
    public int result_id { get; set; }
    public int? exam_id { get; set; }
    public int? student_id { get; set; }
    public int? marks { get; set; }
    public string? grade { get; set; }
}