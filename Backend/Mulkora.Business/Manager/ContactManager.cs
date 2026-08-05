using AutoMapper;
using Mulkora.Business.Abstract;
using Mulkora.DataAccess.Abstract;
using Mulkora.Dto.ContactDtos;
using Mulkora.Entity.Concrete;

namespace Mulkora.Business.Manager;

public class ContactManager : IContactService
{
    private readonly IContactDal _contactDal;
    private readonly IMapper _mapper;


    public ContactManager(IContactDal contactDal, IMapper mapper)
    {
        _contactDal = contactDal;
        _mapper = mapper;
    }

    public async Task<List<ResultContactDto>> TGetListAsync()
    {
        var values = await _contactDal.GetListAsync();
        return _mapper.Map<List<ResultContactDto>>(values);
    }

    public async Task<UpdateContactDto> TGetByIdAsync(int id)
    {
        var value = await _contactDal.GetByIdAsync(id);
        return _mapper.Map<UpdateContactDto>(value);
    }

    public async Task TInsertAsync(CreateContactDto dto)
    {
        var value = _mapper.Map<Contact>(dto);
        await _contactDal.InsertAsync(value);
    }

    public async Task TUpdateAsync(UpdateContactDto dto)
    {
        var value = _mapper.Map<Contact>(dto);
        await _contactDal.UpdateAsync(value);
    }

    public async Task TDeleteAsync(int id)
    {
        await _contactDal.DeleteAsync(id);
    }

    public async Task<List<ResultContactDto>> GetFullListAsync(int page)
    {
        var values = await _contactDal.GetFullListAsync(page);
        return _mapper.Map<List<ResultContactDto>>(values);
    }
}