using Mulkora.Dto.ContactDtos;

namespace Mulkora.WebUI.Services.Abstract;

public interface IContactService : IGenericService<ResultContactDto, CreateContactDto, UpdateContactDto>
{
    Task<List<ResultContactDto>> GetFullListAsync(int page, string token);
    Task<HttpResponseMessage> SendMessageAsync(CreateContactDto dto);
}