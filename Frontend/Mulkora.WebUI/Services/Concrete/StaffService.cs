using Mulkora.Dto.StaffDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class StaffService : GenericService<ResultStaffDto, CreateStaffDto, UpdateStaffDto>, IStaffService
{
    public StaffService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Staffs";
}