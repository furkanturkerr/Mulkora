using Otelvexa.Dto.ServiceDtos;
using Otelvexa.WebUI.Services.Abstract;

namespace Otelvexa.WebUI.Services.Concrete;

public class ServiceService : GenericService<ResultServiceDto, CreateServiceDto, UpdateServiceDto>, IServiceService
{
    public ServiceService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Services";
}