namespace Mulkora.WebUI.Areas.Admin.Models;

public class FeatureOptionViewModel
{
    public int FeatureId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsSelected { get; set; }
}