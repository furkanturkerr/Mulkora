namespace Mulkora.Dto.PropertyImageDtos;

public class CreatePropertyImageDto
{
    public string ImageUrl { get; set; } = string.Empty;
    
    public int DisplayOrder { get; set; }

    public int PropertyId { get; set; }
}