using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.AppointmentDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(appUserId))
            {
                return Unauthorized();
            }

            await _appointmentService.CreateAsync(dto, appUserId);

            return Ok();
        }
    }
}
