using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otelvexa.Business.Abstract;
using Otelvexa.Dto.ServiceDtos;

namespace Otelvexa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _serviceService.TGetListAsync();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _serviceService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceDto dto)
        {
            await _serviceService.TInsertAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateServiceDto dto)
        {
            await _serviceService.TUpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.TDeleteAsync(id);
            return Ok();
        }
    }
}
