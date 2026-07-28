using AutoMapper;
using Otelvexa.Dto.RoomDtos;
using Otelvexa.Dto.ServiceDtos;
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
        
        CreateMap<Room, ResultRoomDto>();
        CreateMap<CreateRoomDto, Room>();
        CreateMap<UpdateRoomDto, Room>().ReverseMap();
        
        CreateMap<Service, ResultServiceDto>();
        CreateMap<CreateServiceDto, Service>();
        CreateMap<UpdateServiceDto, Service>().ReverseMap();
    }
}