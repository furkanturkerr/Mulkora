using Mulkora.Dto.AgentDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class AgentService : GenericService<ResultAgentDto, CreateAgentDto, UpdateAgentDto>, IAgentService
{
    public AgentService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Agents";
}