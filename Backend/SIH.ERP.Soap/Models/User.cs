namespace SIH.ERP.Soap.Models;

public class User
{
    public int user_id { get; set; }
    public string? full_name { get; set; }
    public string? email { get; set; }
    public DateTime? dob { get; set; }
    public string? password_hash { get; set; }
    public bool? is_active { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}