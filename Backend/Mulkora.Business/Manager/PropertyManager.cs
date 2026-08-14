using AutoMapper;
using FluentValidation;
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
    private readonly IValidator<CreatePropertyDto> _createValidator;
    private readonly IValidator<UpdatePropertyDto> _updateValidator;

    public PropertyManager(IPropertyDal propertyDal, IMapper mapper, IValidator<CreatePropertyDto> createValidator, IValidator<UpdatePropertyDto> updateValidator)
    {
        _propertyDal = propertyDal;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<ResultPropertyDto>> TGetListAsync()
    {
        var values = await _propertyDal.GetPropertiesWithFeaturesAsync();
        return _mapper.Map<List<ResultPropertyDto>>(values);
    }
    
    public async Task<List<ResultPropertyDto>> TGetPropertiesByUserIdAsync(string userId, string? text, PropertyStatus? IsStatus)
    {
        var values = await _propertyDal.GetPropertiesByUserIdAsync(userId, text,  IsStatus);
        return _mapper.Map<List<ResultPropertyDto>>(values);
    }

    public async Task<UpdatePropertyDto> TGetByIdAsync(int id)
    {
        var value = await _propertyDal.GetByIdWithFeaturesAsync(id);
        return _mapper.Map<UpdatePropertyDto>(value);
    }

    public Task TInsertAsync(CreatePropertyDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<GetByIdPropertyDto> GetByIdAsync(int id)
    {
        var value = await _propertyDal.GetByIdWithFeaturesAsync(id);
        return _mapper.Map<GetByIdPropertyDto>(value);
    }

    public async Task<int> TAddAsync(CreatePropertyDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var value = _mapper.Map<Property>(dto);

        value.CreatedDate = DateTime.UtcNow;
        value.UpdatedDate = DateTime.UtcNow;
        value.Status = PropertyStatus.Draft;

        await _propertyDal.InsertWithFeaturesAsync(value, dto.FeatureIds);
        return value.PropertyId;
    }

    public async Task TUpdateAsync(UpdatePropertyDto dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var value = await _propertyDal.GetByIdWithFeaturesAsync(dto.PropertyId);

        if (value == null)
        {
            throw new Exception("İlan bulunamadı.");
        }

        if (value.AgentId != dto.AgentId)
        {
            throw new Exception(
                "Bu ilanı güncelleme yetkiniz bulunmuyor.");
        }

        var createdDate = value.CreatedDate;
        var agentId = value.AgentId;

        _mapper.Map(dto, value);

        value.CreatedDate = createdDate;
        value.Status = PropertyStatus.Draft;
        value.AgentId = agentId;
        value.UpdatedDate = DateTime.UtcNow;

        await _propertyDal.UpdateWithFeaturesAsync(value, dto.FeatureIds);
    }

    public async Task<List<ResultPropertyDto>> GetFilterProperty(string? text, PropertyStatus? IsStatus, string? City, string? District, ListingType? ListingType, int page, int pageSize)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 8;
        }

        var totalCount = await _propertyDal.GetFilterPropertyCount(text, IsStatus, City, District, ListingType);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (totalPages > 0 && page > totalPages)
        {
            page = totalPages;
        }

        var properties = await _propertyDal.GetFilterProperty(text, IsStatus, City, District, ListingType, page, pageSize);
        var values = _mapper.Map<List<ResultPropertyDto>>(properties);

        foreach (var value in values)
        {
            value.CurrentPage = page;
            value.TotalPages = totalPages;
        }

        return values;
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
        var property = await _propertyDal.GetByIdAsync(id);
        
        if (property == null)
        throw new Exception("İlan bulunamadı.");
        
        if (property.Status != PropertyStatus.PendingApproval)
            throw new Exception("Sadece onay bekleyen ilanlar onaylanabilir.");
        
        property.Status = PropertyStatus.Published;
        await _propertyDal.UpdateAsync(property);
    }

    public async Task TRejectAsync(int id)
    {
        var property = await _propertyDal.GetByIdAsync(id);

        if (property == null)
            throw new Exception("İlan bulunamadı.");

        if (property.Status != PropertyStatus.PendingApproval)
            throw new Exception("Sadece onay bekleyen ilanlar reddedilebilir.");

        property.Status = PropertyStatus.Rejected;

        await _propertyDal.UpdateAsync(property);
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
}