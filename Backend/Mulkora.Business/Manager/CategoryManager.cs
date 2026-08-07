using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.CategoryDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
    }

    public async Task<List<ResultCategoryDto>> TGetListAsync()
    {
        var values = await _categoryDal.GetListAsync();
        return _mapper.Map<List<ResultCategoryDto>>(values);
    }

    public async Task<UpdateCategoryDto> TGetByIdAsync(int id)
    {
        var value = await _categoryDal.GetByIdAsync(id);
        return _mapper.Map<UpdateCategoryDto>(value);
    }

    public async Task TInsertAsync(CreateCategoryDto dto)
    {
        var value = _mapper.Map<Category>(dto);
        await _categoryDal.InsertAsync(value);
    }

    public async Task TUpdateAsync(UpdateCategoryDto dto)
    {
        var value = _mapper.Map<Category>(dto);
        await _categoryDal.UpdateAsync(value);
    }

    public async Task TDeleteAsync(int id)
    {
        await _categoryDal.DeleteAsync(id);
    }
}