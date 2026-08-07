using Mulkora.Dto.FeatureDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class FeatureService : GenericService<ResultFeatureDto, CreateFeatureDto, UpdateFeatureDto>, IFeatureService
{
    public FeatureService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Features";
}