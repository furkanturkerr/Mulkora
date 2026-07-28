using AutoMapper;
using Otelvexa.Business.Abstract;
using Otelvexa.DataAccess.Abstract;
using Otelvexa.Dto.RoomDtos;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.Business.Manager;

public class RoomManager : IRoomService
{
    private readonly IRoomDal _roomDal;
    private readonly IMapper _mapper;

    public RoomManager(IRoomDal roomDal, IMapper mapper)
    {
        _roomDal = roomDal;
        _mapper = mapper;
    }

    public async Task<List<ResultRoomDto>> TGetListAsync()
    {
        var values = await _roomDal.GetListAsync();
        return _mapper.Map<List<ResultRoomDto>>(values);
    }

    public async Task<UpdateRoomDto> TGetByIdAsync(int id)
    {
        var values = await _roomDal.GetByIdAsync(id);
        return _mapper.Map<UpdateRoomDto>(values);
    }

    public async Task TInsertAsync(CreateRoomDto dto)
    {
        var values = _mapper.Map<Room>(dto);
        await _roomDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateRoomDto dto)
    {
        var values = _mapper.Map<Room>(dto);
        await _roomDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _roomDal.DeleteAsync(id);
    }
}