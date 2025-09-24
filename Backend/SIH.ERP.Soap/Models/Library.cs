namespace SIH.ERP.Soap.Models;

public class Library
{
    public int book_id { get; set; }
    public string? title { get; set; }
    public string? author { get; set; }
    public string? shelf { get; set; }
    public string? isbn { get; set; }
    public int? copies { get; set; }
}