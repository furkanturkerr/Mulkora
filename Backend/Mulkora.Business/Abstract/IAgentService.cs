using Microsoft.AspNetCore.Identity;
using Mulkora.Dto.AgentDtos;

namespace Mulkora.Business.Abstract;

public interface IAgentService
{
    Task<IdentityResult> CreateAgentAsync(CreateAgentDto dto);
    Task<IdentityResult> UpdateAgentAsync(UpdateAgentDto dto);
    Task<List<ResultAgentDto>> GetAllAsync();
    Task<UpdateAgentDto> GetByIdAsync(int id);
    Task<UpdateAgentDto> GetByUserIdAsync(int id);
    Task<IdentityResult> DeleteAgentAsync(int id);
    Task<List<ResultAgentDto>> TGetListAgentTrue();
    Task<List<ResultAgentDto>> TGetFilterAgent(string? text, bool? isTrue);
}