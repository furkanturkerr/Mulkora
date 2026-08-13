namespace Mulkora.WebUI.Models;

public class FeatureOptionViewModel
{
    public int FeatureId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsSelected { get; set; }
}