using AutoMapper;
using Mulkora.Dto.AgentDtos;
using Mulkora.Dto.CategoryDtos;
using Mulkora.Dto.ContactDtos;
using Mulkora.Dto.FeatureDtos;
using Mulkora.Dto.PropertyDtos;
using Mulkora.Dto.PropertyImageDtos;
using Mulkora.Dto.RoleDtos;
using Mulkora.Dto.UserDtos;
using Mulkora.Entity.Concrete;
using Mulkora.Entity.Enums;

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

        CreateMap<Property, ResultPropertyDto>()
            .ForMember(destination => destination.AgentName, options => options.MapFrom(source => source.Agent.AppUser.Name))
            .ForMember(destination => destination.ImageUrl, options => options.MapFrom(source =>
                source.PropertyImages
                    .OrderBy(x => x.PropertyImageId)
                    .Select(x => x.ImageUrl)
                    .FirstOrDefault() ?? string.Empty
            ));
        
        CreateMap<CreatePropertyDto, Property>()
            .ForMember(
                destination => destination.Features,
                options => options.Ignore());

        CreateMap<UpdatePropertyDto, Property>()
            .ForMember(
                destination => destination.Features,
                options => options.Ignore());
        //ıgnore : Yani normal alanları map et, ilişkiyi ayrıca kur.
        
        CreateMap<Property, GetByIdPropertyDto>()
            .ForMember(destination => destination.AgentName, options => options.MapFrom(source => source.Agent.AppUser.Name + " " + source.Agent.AppUser.Surname))
            .ForMember(destination => destination.AgentTitle, options => options.MapFrom(source => source.Agent.Title))
            .ForMember(destination => destination.AgentImageUrl, options => options.MapFrom(source => source.Agent.ImageUrl));
        
        CreateMap<AppUser, ResultUserDto>();
        
        CreateMap<PropertyImage, ResultPropertyImageDto>();
        CreateMap<CreatePropertyImagesDto, PropertyImage>();
        CreateMap<UpdatePropertyImageDto, PropertyImage>().ReverseMap();
        


    }
}