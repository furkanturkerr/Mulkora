using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.RoleDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _roleService.GetAllRoles();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var value = await _roleService.GetRoleByIdAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            var result = await _roleService.CreateRoleAsync(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Role", error.Description);

                return ValidationProblem(ModelState);
            }
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleDto dto)
        {
            var result = await _roleService.UpdateRoleAsync(dto);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Role", error.Description);

                return ValidationProblem(ModelState);
            }
            
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(string id)
        {
            var value = await _roleService.DeleteRoleAsync(id);
            return Ok(value);
        }
    }
}
