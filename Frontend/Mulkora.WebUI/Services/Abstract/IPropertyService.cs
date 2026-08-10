using Mulkora.Dto.PropertyDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IPropertyService : IGenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>
{
    Task<List<ResultPropertyDto>> GetPropertiesByUserIdAsync(string userId);
    Task CreatePropertyAsync(CreatePropertyDto dto, string token);
    Task SendForApprovalAsync(int id, string token);
    Task<GetByIdPropertyDto> GetByIdAsync(int id);
}