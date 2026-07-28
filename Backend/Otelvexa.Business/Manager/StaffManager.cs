using AutoMapper;
using Otelvexa.Business.Abstract;
using Otelvexa.DataAccess.Abstract;
using Otelvexa.DataAccess.EntityFramework;
using Otelvexa.Dto.StaffDtos;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.Business.Manager;

public class StaffManager : IStaffService
{
    private readonly IStaffDal _staffDal;
    private readonly IMapper _mapper;


    public StaffManager(IStaffDal staffDal, IMapper mapper)
    {
        _staffDal = staffDal;
        _mapper = mapper;
    }

    public async Task<List<ResultStaffDto>> TGetListAsync()
    {
        var staffs = await _staffDal.GetListAsync();
        return _mapper.Map<List<ResultStaffDto>>(staffs);
    }

    public async Task<UpdateStaffDto> TGetByIdAsync(int id)
    {
        var staff = await _staffDal.GetByIdAsync(id);
        return _mapper.Map<UpdateStaffDto>(staff);
    }

    public async Task TInsertAsync(CreateStaffDto dto)
    {
        var values = _mapper.Map<Staff>(dto);
        await _staffDal.InsertAsync(values);
    }

    public async Task TUpdateAsync(UpdateStaffDto dto)
    {
        var values = _mapper.Map<Staff>(dto);
        await _staffDal.UpdateAsync(values);
    }

    public async Task TDeleteAsync(int id)
    {
        await _staffDal.DeleteAsync(id);
    }
}