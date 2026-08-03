using Mulkora.Dto.ContactDtos;

namespace Mulkora.Business.Abstract;

public interface IContactService : IGenericService<ResultContactDto, CreateContactDto, UpdateContactDto>
{
    
}