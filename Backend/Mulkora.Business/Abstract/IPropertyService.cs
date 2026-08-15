using Mulkora.Dto.PropertyDtos;
using Mulkora.Entity.Enums;

namespace Mulkora.Business.Abstract;

public interface IPropertyService : IGenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>
{
    Task<List<ResultPropertyDto>> TGetPropertiesByUserIdAsync(string userId, string? text, PropertyStatus? IsStatus);
    Task TSendForApprovalAsync(int id, int agentId);
    Task TApproveAsync(int id);
    Task TRejectAsync(int id);
    Task TMakePassiveAsync(int id, int agentId);
    Task TMarkAsSoldAsync(int id, int agentId);
    Task TMarkAsRentedAsync(int id, int agentId);
    Task<GetByIdPropertyDto> GetByIdAsync(int id);
    Task<int> TAddAsync(CreatePropertyDto dto);
    Task TUpdateAsync(UpdatePropertyDto dto);
    Task<List<ResultPropertyDto>> GetFilterProperty(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType, int page, int pageSize);
    Task<List<ResultPropertyDto>> GetFilterPropertyAll(string? city, string? district, ListingType? listingType, int? maxPrice, int? minPrice, int? categoryId, int? roomCount, int page, int pageSize);
    Task<GetByIdPropertyDto?> GetPublishedByIdAsync(int id);
}