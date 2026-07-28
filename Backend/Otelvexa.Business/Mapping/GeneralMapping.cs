using AutoMapper;
using Otelvexa.Dto.StaffDtos;
using Otelvexa.Dto.TestimonialDtos;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.Business.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Staff, ResultStaffDto>();
        CreateMap<CreateStaffDto, Staff>();
        CreateMap<UpdateStaffDto, Staff>().ReverseMap();
        
        CreateMap<Testimonial, ResultTestimonialDto>();
        CreateMap<CreateTestimonialDto, Testimonial>();
        CreateMap<UpdateTestimonialDto, Testimonial>().ReverseMap();
    }
}