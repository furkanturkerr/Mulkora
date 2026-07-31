using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.RoomDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

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