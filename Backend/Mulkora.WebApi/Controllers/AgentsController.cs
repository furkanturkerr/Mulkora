using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.AgentDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var agents = await _agentService.GetAllAsync();
            return Ok(agents);
        }
        
        [HttpGet("true")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListAgentTrue()
        {
            var agents = await _agentService.TGetListAgentTrue();
            return Ok(agents);
        }
        
        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> GetFilterAgent(string? text, bool? isTrue)
        {
            var agents = await _agentService.TGetFilterAgent(text, isTrue);
            return Ok(agents);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _agentService.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("agent/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAgentById(int id)
        {
            var value = await _agentService.GetByUserIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateAgentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var result = await _agentService.CreateAgentAsync(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Agent", error.Description);

                return ValidationProblem(ModelState);
            }

            return Ok(new { message = "Danışman başarıyla oluşturuldu." });
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateAgentDto dto)
        {
            var result = await _agentService.UpdateAgentAsync(dto);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Agent", error.Description);
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _agentService.DeleteAgentAsync(id);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Agent", error.Description);

                return ValidationProblem(ModelState);
            }

            return NoContent();
        }
    }
}
