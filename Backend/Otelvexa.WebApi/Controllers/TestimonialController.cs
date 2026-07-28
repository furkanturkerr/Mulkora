using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otelvexa.Business.Abstract;
using Otelvexa.Dto.TestimonialDtos;

namespace Otelvexa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _testimonialService.TGetListAsync();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _testimonialService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialDto dto)
        {
            await _testimonialService.TInsertAsync(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTestimonialDto dto)
        {
            await _testimonialService.TUpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _testimonialService.TDeleteAsync(id);
            return Ok();
        }
    }
}
