using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.DataAccess.EntityFramework;
using Mulkora.Dto.ServiceDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class ServiceManager : IServiceService
{
    private readonly IServicesDal _serviceDal;
    private readonly IMapper _mapper;

    public ServiceManager(IServicesDal serviceDal, IMapper mapper)
    {
        _serviceDal = serviceDal;
        _mapper = mapper;
    }


    public async Task<List<ResultServiceDto>> TGetListAsync()
    {
        var values = await _serviceDal.GetListAsync();
        return _mapper.Map<List<ResultServiceDto>>(values);
    }

    public async Task<UpdateServiceDto> TGetByIdAsync(int id)
    {
        var values = await _serviceDal.GetByIdAsync(id);
        return _mapper.Map<UpdateServiceDto>(values);
    }

    public async Task TInsertAsync(CreateServiceDto dto)
    {
        var values = _mapper.Map<Service>(dto);
        await _serviceDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateServiceDto dto)
    {
        var values = _mapper.Map<Service>(dto);
        await _serviceDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _serviceDal.DeleteAsync(id);
    }
}