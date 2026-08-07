namespace Mulkora.Dto.CategoryDtos;

public class ResultCategoryDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}