using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.PropertyDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _propertyService.TGetListAsync();
            return Ok(values);
        }
        
        [Authorize(Roles = "Agent")]
        [HttpGet("my-properties")]
        public async Task<IActionResult> GetMyProperties()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var values = await _propertyService.TGetPropertiesByUserIdAsync(userId);

            return Ok(values);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _propertyService.GetByIdAsync(id);
            return Ok(value);
        }
        
        [Authorize(Roles = "Agent")]
        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto dto)
        {
            var agentIdClaim = User.FindFirstValue("AgentId");

            if (!int.TryParse(agentIdClaim, out var agentId))
            {
                return Unauthorized();
            }

            dto.AgentId = agentId;

            await _propertyService.TInsertAsync(dto);

            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePropertyDto dto)
        {
            await _propertyService.TUpdateAsync(dto);
            return Ok();      
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _propertyService.TDeleteAsync(id);
            return Ok();      
        }
        
        [HttpPatch("{id}/send-for-approval")]
        public async Task<IActionResult> SendForApproval(int id)
        {
            var agentIdClaim = User.FindFirstValue("AgentId");

            if (string.IsNullOrEmpty(agentIdClaim))
                return Unauthorized();

            var agentId = int.Parse(agentIdClaim);

            await _propertyService.TSendForApprovalAsync(id, agentId);

            return Ok();
        }
        
        [HttpPatch("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            await _propertyService.TApproveAsync(id);
            return Ok();
        }
        
        [HttpPatch("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            await _propertyService.TRejectAsync(id);
            return Ok();
        }
    }
}
