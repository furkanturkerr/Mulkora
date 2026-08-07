namespace Mulkora.Dto.CategoryDtos;

public class UpdateCategoryDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}