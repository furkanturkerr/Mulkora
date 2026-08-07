namespace Mulkora.Dto.PropertyImageDtos;

public class CreatePropertyImageDto
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsCover { get; set; }
    public int DisplayOrder { get; set; }
}