using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.PropertyDtos;
using Mulkora.Entity.Enums;
using Mulkora.WebApi.Services.OpenAIServices;
using Mulkora.WebApi.Services.TrueWayGeocodingServices;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IOpenAIService _openAIService;
        private readonly ITrueWayGeocodingService _geocodingService;

        public PropertiesController(IPropertyService propertyService, IOpenAIService openAıService, ITrueWayGeocodingService geocodingService)
        {
            _propertyService = propertyService;
            _openAIService = openAıService;
            _geocodingService = geocodingService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> Get()
        {
            var values = await _propertyService.TGetListAsync();
            return Ok(values);
        }

        [HttpGet("filter")]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> GetFilter(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType, int page = 1, int pageSize = 8)
        {
            var values = await _propertyService.GetFilterProperty(text, IsStatus, City, District, ListingType, page, pageSize);

            return Ok(values);
        }
        
        [HttpGet("filterAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string? city, string? district, ListingType? listingType, int? maxPrice, int? minPrice,
            int? categoryId, int? roomCount, int page = 1, int pageSize = 8)
        {
            var values = await _propertyService.GetFilterPropertyAll(city, district, listingType, maxPrice, minPrice, categoryId, roomCount, page, pageSize);
            return Ok(values);
        }
        
        [AllowAnonymous]
        [HttpGet("published/{id:int}")]
        public async Task<IActionResult> GetPublishedById(int id)
        {
            var property = await _propertyService.GetPublishedByIdAsync(id);

            if (property == null)
            {
                return NotFound("Yayında olan ilan bulunamadı.");
            }

            return Ok(property);
        }
        
        [Authorize(Roles = "Agent")]
        [HttpGet("my-properties")]
        public async Task<IActionResult> GetMyProperties(string? text, PropertyStatus? IsStatus)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var values = await _propertyService.TGetPropertiesByUserIdAsync(userId!, text, IsStatus);

            return Ok(values);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _propertyService.GetByIdAsync(id);

            try
            {
                value.AiIsApproved = await _openAIService.CheckPropertyAsync(value);
            }
            catch
            {
                value.AiIsApproved = null;
            }
            
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
            
            var coordinates = await _geocodingService.GetCoordinatesAsync(dto.City, dto.District, dto.Address);

            if (coordinates != null)
            {
                dto.Latitude = coordinates.Latitude;
                dto.Longitude = coordinates.Longitude;
            }

            var propertyId = await _propertyService.TAddAsync(dto);

            return Ok(propertyId);
        }
        
        [Authorize(Roles = "Agent,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePropertyDto dto)
        {
            var agentIdClaim = User.FindFirstValue("AgentId");

            if (!int.TryParse(agentIdClaim, out var agentId))
            {
                return Unauthorized();
            }

            dto.AgentId = agentId;
            
            
            var coordinates =
                await _geocodingService.GetCoordinatesAsync(
                    dto.City,
                    dto.District,
                    dto.Address
                );

            if (coordinates != null)
            {
                dto.Latitude = coordinates.Latitude;
                dto.Longitude = coordinates.Longitude;
            }
            
            await _propertyService.TUpdateAsync(dto);
            return Ok();      
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Agent,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _propertyService.TDeleteAsync(id);
            return Ok();      
        }
        
        [HttpPatch("{id}/send-for-approval")]
        [Authorize(Roles = "Agent")]
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
