using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.PropertyDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class PropertyManager : IPropertyService
{
    private readonly IPropertyDal _propertyDal;
    private readonly IMapper _mapper;   

    public PropertyManager(IPropertyDal propertyDal, IMapper mapper)
    {
        _propertyDal = propertyDal;
        _mapper = mapper;
    }

    public async Task<List<ResultPropertyDto>> TGetListAsync()
    {
        var values = await _propertyDal.GetListAsync();
        return _mapper.Map<List<ResultPropertyDto>>(values);
    }

    public async Task<UpdatePropertyDto> TGetByIdAsync(int id)
    {
        var value = await _propertyDal.GetByIdAsync(id);
        return _mapper.Map<UpdatePropertyDto>(value);
    }

    public async Task TInsertAsync(CreatePropertyDto dto)
    {
        var value = _mapper.Map<Property>(dto);
        await _propertyDal.InsertAsync(value);
    }

    public async Task TUpdateAsync(UpdatePropertyDto dto)
    {
        var value = _mapper.Map<Property>(dto);
        await _propertyDal.UpdateAsync(value);
    }

    public async Task TDeleteAsync(int id)
    {
        await _propertyDal.DeleteAsync(id);
    }
}