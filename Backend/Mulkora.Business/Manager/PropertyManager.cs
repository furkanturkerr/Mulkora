using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.PropertyDtos;
using Mulkora.Entity.Concrete;
using Mulkora.Entity.Enums;

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
    
    public async Task<List<ResultPropertyDto>> TGetPropertiesByUserIdAsync(string userId)
    {
        var values = await _propertyDal.GetPropertiesByUserIdAsync(userId);
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
        value.Status = PropertyStatus.Draft;
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
    
    public async Task TSendForApprovalAsync(int id, int agentId)
    {
        var property = await _propertyDal.GetByIdAsync(id);

        if (property == null)
            throw new Exception("İlan bulunamadı.");
        
        if (property.AgentId != agentId)
            throw new UnauthorizedAccessException("Bu ilan üzerinde işlem yapamazsınız.");

        var canSendForApproval =
            property.Status == PropertyStatus.Draft ||
            property.Status == PropertyStatus.Rejected;

        if (!canSendForApproval)
            throw new Exception("Bu ilan onaya gönderilemez.");
        
        property.Status = PropertyStatus.PendingApproval;
        await _propertyDal.UpdateAsync(property);
    }

    public async Task TApproveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task TRejectAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task TMakePassiveAsync(int id, int agentId)
    {
        throw new NotImplementedException();
    }

    public async Task TMarkAsSoldAsync(int id, int agentId)
    {
        throw new NotImplementedException();
    }

    public async Task TMarkAsRentedAsync(int id, int agentId)
    {
        throw new NotImplementedException();
    }

    public async Task<GetByIdPropertyDto> GetByIdAsync(int id)
    {
        var value = await _propertyDal.GetByIdAsync(id);
        return _mapper.Map<GetByIdPropertyDto>(value);
    }
}