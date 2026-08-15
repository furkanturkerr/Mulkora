using Mulkora.Dto.PropertyDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IPropertyService : IGenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>
{
    Task<List<ResultPropertyDto>> GetPropertiesByUserIdAsync(string userId, string? text, int? IsStatus);
    Task<HttpResponseMessage> CreatePropertyAsync(CreatePropertyDto dto, string token);
    Task SendForApprovalAsync(int id, string token);
    Task ApproveAsync(int id, string token);
    Task RejectAsync(int id, string token);
    Task<GetByIdPropertyDto> GetByIdAsync(int id);
    Task<HttpResponseMessage> UpdatePropertyAsync(UpdatePropertyDto dto, string token);
    Task<List<ResultPropertyDto>> GetFilterProperty(string? text, int? IsStatus, string? City, string? District, int? ListingType, int page, int pageSize, string token);
    Task<List<ResultPropertyDto>> GetFilterPropertyAll(string? city, string? district, int? listingType, int? maxPrice, int? minPrice, int? categoryId, int? roomCount, int page, int pageSize);
    Task<GetByIdPropertyDto?> GetPublishedByIdAsync(int id);
}