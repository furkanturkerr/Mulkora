using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.UserDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _userService.TGetListAsync();
            return Ok(values);
        }
        
        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var value = await _userService.GetUserRolesAsync(id);

            return Ok(value);
        }

        [HttpPut("roles")]
        public async Task<IActionResult> UpdateUserRoles(RoleAssignDto dto)
        {
            await _userService.UpdateUserRolesAsync(dto);

            return Ok();
        }
        
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserDto dto)
        {
            await _userService.TUpdateAsync(dto);
            return Ok();      
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string id)
        {
            var values = await _userService.GetUserByIdAsync(id);
            return Ok(values);
        } 
    }
}
