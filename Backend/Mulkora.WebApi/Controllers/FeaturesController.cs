using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.FeatureDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _featureService.TGetListAsync();
            return Ok(values);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _featureService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateFeatureDto dto)
        {
            await _featureService.TInsertAsync(dto);
            return Ok();
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateFeatureDto dto)
        {
            await _featureService.TUpdateAsync(dto);
            return Ok();      
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.TDeleteAsync(id);
            return Ok();       
        }
    }
}
