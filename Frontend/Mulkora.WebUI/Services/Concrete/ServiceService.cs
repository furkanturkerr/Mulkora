using Mulkora.Dto.ServiceDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class ServiceService : GenericService<ResultServiceDto, CreateServiceDto, UpdateServiceDto>, IServiceService
{
    public ServiceService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Services";
}