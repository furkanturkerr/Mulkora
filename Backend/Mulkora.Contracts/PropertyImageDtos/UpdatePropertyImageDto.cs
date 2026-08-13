namespace Mulkora.Dto.PropertyImageDtos;

public class UpdatePropertyImageDto
{
    public int PropertyImageId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    
    public int DisplayOrder { get; set; }
}