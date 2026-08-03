namespace Mulkora.Entity.Concrete;

public class Contact
{
    public int ContactId { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public DateTime MessageDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string NameSurname { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
}