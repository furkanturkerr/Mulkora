using Mulkora.Dto.RoomDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class RoomService : GenericService<ResultRoomDto, CreateRoomDto, UpdateRoomDto>, IRoomService
{
    public RoomService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Rooms";
}