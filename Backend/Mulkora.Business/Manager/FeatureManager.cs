using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.FeatureDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class FeatureManager : IFeatureService
{
    private readonly IFeatureDal _featureDal;
    private readonly IMapper _mapper;

    public FeatureManager(IFeatureDal featureDal, IMapper mapper)
    {
        _featureDal = featureDal;
        _mapper = mapper;
    }

    public async Task<List<ResultFeatureDto>> TGetListAsync()
    {
        var values = await _featureDal.GetListAsync();
        return _mapper.Map<List<ResultFeatureDto>>(values);
    }

    public async Task<UpdateFeatureDto> TGetByIdAsync(int id)
    {
        var value = await _featureDal.GetByIdAsync(id);
        return _mapper.Map<UpdateFeatureDto>(value);
    }

    public async Task TInsertAsync(CreateFeatureDto dto)
    {
        var value = _mapper.Map<Feature>(dto);
        await _featureDal.InsertAsync(value);
    }

    public async Task TUpdateAsync(UpdateFeatureDto dto)
    {
        var value = _mapper.Map<Feature>(dto);
        await _featureDal.UpdateAsync(value);
    }

    public async Task TDeleteAsync(int id)
    {
        await _featureDal.DeleteAsync(id);
    }
}