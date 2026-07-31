using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.TestimonialDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class TestimonialManager : ITestimonialService
{
    private readonly ITestimonialDal _testimonialDal;
    private readonly IMapper _mapper;

    public TestimonialManager(ITestimonialDal testimonialDal, IMapper mapper)
    {
        _testimonialDal = testimonialDal;
        _mapper = mapper;
    }

    public async Task<List<ResultTestimonialDto>> TGetListAsync()
    {
        var results = await _testimonialDal.GetListAsync();
        return _mapper.Map<List<ResultTestimonialDto>>(results);
    }

    public async Task<UpdateTestimonialDto> TGetByIdAsync(int id)
    {
        var value = await _testimonialDal.GetByIdAsync(id);
        return _mapper.Map<UpdateTestimonialDto>(value);
    }

    public async Task TInsertAsync(CreateTestimonialDto dto)
    {
        var values = _mapper.Map<Testimonial>(dto);
        await _testimonialDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateTestimonialDto dto)
    {
        var values = _mapper.Map<Testimonial>(dto);
        await _testimonialDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _testimonialDal.DeleteAsync(id);
    }
}