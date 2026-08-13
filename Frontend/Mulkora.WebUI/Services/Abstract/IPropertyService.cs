using Mulkora.Dto.PropertyDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IPropertyService : IGenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>
{
    Task<List<ResultPropertyDto>> GetPropertiesByUserIdAsync(string userId);
    Task<int> CreatePropertyAsync(CreatePropertyDto dto, string token);
    Task SendForApprovalAsync(int id, string token);
    Task ApproveAsync(int id, string token);
    Task RejectAsync(int id, string token);
    Task<GetByIdPropertyDto> GetByIdAsync(int id);
    Task UpdatePropertyAsync(UpdatePropertyDto dto, string token);
}