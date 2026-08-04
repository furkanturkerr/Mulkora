using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Role", error.Description);

                return ValidationProblem(ModelState);
            }
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, string roleName)
        {
            var result = await _roleService.UpdateRoleAsync(id, roleName);
            
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
