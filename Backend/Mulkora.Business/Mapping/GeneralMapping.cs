using AutoMapper;
using Mulkora.Dto.RoomDtos;
using Mulkora.Dto.ServiceDtos;
using Mulkora.Dto.StaffDtos;
using Mulkora.Dto.TestimonialDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Mapping;

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