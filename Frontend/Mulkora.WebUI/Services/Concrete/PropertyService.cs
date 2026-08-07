using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class PropertyService : GenericService<ResultPropertyDto, CreatePropertyDto, UpdatePropertyDto>, IPropertyService
{
    public PropertyService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }
    
    protected override string ApiRoute => "api/Properties";
}