namespace SIH.ERP.Soap.Models;

public class ContactDetails
{
    public int contact_id { get; set; }
    public int? user_id { get; set; }
    public string? contact_type { get; set; }
    public string? contact_value { get; set; }
    public bool? is_primary { get; set; }
}