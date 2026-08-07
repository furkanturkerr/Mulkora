namespace Mulkora.Entity.Concrete;

public class Feature
{
    public int FeatureId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<Property> Properties { get; set; } = new List<Property>();
}