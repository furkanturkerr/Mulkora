using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.RoomDtos;

namespace Mulkora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService ıroomService)
        {
            _roomService = ıroomService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _roomService.TGetListAsync();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _roomService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomDto createRoomDto)
        {
            await _roomService.TInsertAsync(createRoomDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoomDto updateRoomDto)
        {
            await _roomService.TUpdateAsync(updateRoomDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.TDeleteAsync(id);
            return Ok();
        }
    }
}
