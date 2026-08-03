namespace Mulkora.Dto.ContactDtos;

public class CreateContactDto
{
    public string MessageText { get; set; } 
    public DateTime MessageDate { get; set; }
    public string Email { get; set; } 
    public string NameSurname { get; set; } 
    public string Subject { get; set; }
}