using Microsoft.AspNetCore.Http;
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
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _propertyService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto dto)
        {
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
    }
}
