using Mulkora.Dto.ContactDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class ContactService : GenericService<ResultContactDto, CreateContactDto, UpdateContactDto>, IContactService
{
    public ContactService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Contacts";
}