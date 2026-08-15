using Mulkora.Dto.AgentDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IAgentService : IGenericService<ResultAgentDto, CreateAgentDto, UpdateAgentDto>
{
    Task<List<ResultAgentDto>> GetListAgentTrue();
    Task<List<ResultAgentDto>> GetFilterAgent(string? text, bool? isTrue, string token);
    Task<UpdateAgentDto> GetAgentByIdAsync(int id);
}