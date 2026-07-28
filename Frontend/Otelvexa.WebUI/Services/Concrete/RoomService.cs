using Otelvexa.Dto.RoomDtos;
using Otelvexa.WebUI.Services.Abstract;

namespace Otelvexa.WebUI.Services.Concrete;

public class RoomService : GenericService<ResultRoomDto, CreateRoomDto, UpdateRoomDto>, IRoomService
{
    public RoomService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Rooms";
}