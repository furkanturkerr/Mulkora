using Otelvexa.Dto.StaffDtos;

namespace Otelvexa.Business.Abstract;

public interface IStaffService : IGenericService<ResultStaffDto, CreateStaffDto, UpdateStaffDto>
{
    
}