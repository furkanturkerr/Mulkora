namespace Mulkora.Dto.PropertyImageDtos;

public class CreatePropertyImagesDto
{
    public int PropertyId { get; set; }

    public List<string> ImageUrls { get; set; } = new List<string>();
}