using AutoMapper;
using Mulkora.Dto.ContactDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateContactDto, Contact>();
        CreateMap<UpdateContactDto, Contact>();
        CreateMap<Contact, ResultContactDto>().ReverseMap();
    }
}