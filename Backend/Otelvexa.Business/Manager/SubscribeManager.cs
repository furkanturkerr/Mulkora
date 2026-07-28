using AutoMapper;
using Otelvexa.Business.Abstract;
using Otelvexa.DataAccess.Abstract;
using Otelvexa.Dto.SubscribeDtos;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.Business.Manager;

public class SubscribeManager : ISubscribeService
{
    private readonly ISubscribeDal  _subscribeDal;
    private readonly IMapper _mapper;

    public SubscribeManager(ISubscribeDal subscribeDal, IMapper mapper)
    {
        _subscribeDal = subscribeDal;
        _mapper = mapper;
    }

    public async Task<List<ResultSubscribeDto>> TGetListAsync()
    {
        var values = await _subscribeDal.GetListAsync();
        return _mapper.Map<List<ResultSubscribeDto>>(values);
    }

    public async Task<UpdateSubscribeDto> TGetByIdAsync(int id)
    {
        var values = await _subscribeDal.GetByIdAsync(id);
        return _mapper.Map<UpdateSubscribeDto>(values);
    }

    public async Task TInsertAsync(CreateSubscribeDto dto)
    {
        var subscribe = _mapper.Map<Subscribe>(dto);
        await _subscribeDal.InsertAsync(subscribe);
    }

    public async Task TUpdateAsync(UpdateSubscribeDto dto)
    {
        var subscribe = _mapper.Map<Subscribe>(dto);
        await _subscribeDal.UpdateAsync(subscribe);
    }

    public async Task TDeleteAsync(int id)
    {
        await _subscribeDal.DeleteAsync(id);
    }
}