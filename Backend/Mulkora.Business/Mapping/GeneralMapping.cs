using AutoMapper;
using Mulkora.Dto.AgentDtos;
using Mulkora.Dto.CategoryDtos;
using Mulkora.Dto.ContactDtos;
using Mulkora.Dto.FeatureDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateContactDto, Contact>();
        CreateMap<UpdateContactDto, Contact>();
        CreateMap<Contact, ResultContactDto>().ReverseMap();
        
        CreateMap<Agent, ResultAgentDto>()
            .ForMember(destination => destination.Name, option => option.MapFrom(source => source.AppUser.Name))
            .ForMember(destination => destination.Surname, option => option.MapFrom(source => source.AppUser.Surname))
            .ForMember(destination => destination.Email, option => option.MapFrom(source => source.AppUser.Email));
        CreateMap<CreateAgentDto, Agent>();
        CreateMap<UpdateAgentDto, Agent>();
        CreateMap<Agent, UpdateAgentDto >()
            .ForMember(destination => destination.Name, option => option.MapFrom(source => source.AppUser.Name))
            .ForMember(destination => destination.Surname, option => option.MapFrom(source => source.AppUser.Surname))
            .ForMember(destination => destination.Email, option => option.MapFrom(source => source.AppUser.Email));

        CreateMap<Category, ResultCategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>().ReverseMap();

        CreateMap<Feature, ResultFeatureDto>();
        CreateMap<CreateFeatureDto, Feature>();
        CreateMap<UpdateFeatureDto, Feature>().ReverseMap();
    }
}