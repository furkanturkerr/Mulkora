namespace Mulkora.Dto.ContactDtos;

public class UpdateContactDto
{
    public int ContactId { get; set; }
    public string MessageText { get; set; }
    public DateTime MessageDate { get; set; }
    public string Email { get; set; } 
    public string NameSurname { get; set; } 
    public string Subject { get; set; } 
}