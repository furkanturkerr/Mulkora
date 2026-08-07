namespace Mulkora.Entity.Concrete;

public class PropertyImage
{
    public int PropertyImageId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsCover { get; set; }

    public int DisplayOrder { get; set; }
}