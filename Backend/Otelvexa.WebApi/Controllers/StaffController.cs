using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otelvexa.Business.Abstract;
using Otelvexa.Dto.StaffDtos;

namespace Otelvexa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _staffService.TGetListAsync();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _staffService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStaffDto dto)
        {
            await _staffService.TInsertAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStaffDto dto)
        {
            await _staffService.TUpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _staffService.TDeleteAsync(id);
            return Ok();
        }
    }
}
