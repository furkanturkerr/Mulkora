using Mulkora.Dto.PropertyDtos;

namespace Mulkora.Business.Abstract;

public interface IPropertyService : IGenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>
{
    Task<List<ResultPropertyDto>> TGetPropertiesByUserIdAsync(string userId);
    Task TSendForApprovalAsync(int id, int agentId);
    Task TApproveAsync(int id);
    Task TRejectAsync(int id);
    Task TMakePassiveAsync(int id, int agentId);
    Task TMarkAsSoldAsync(int id, int agentId);
    Task TMarkAsRentedAsync(int id, int agentId);
    Task<GetByIdPropertyDto> GetByIdAsync(int id);
    
    
    Task<int> TAddAsync(CreatePropertyDto dto);

    Task TUpdateAsync(UpdatePropertyDto dto);
}