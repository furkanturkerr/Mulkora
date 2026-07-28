using Otelvexa.Dto.StaffDtos;
using Otelvexa.WebUI.Services.Abstract;

namespace Otelvexa.WebUI.Services.Concrete;

public class StaffService : GenericService<ResultStaffDto, CreateStaffDto, UpdateStaffDto>, IStaffService
{
    public StaffService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Staffs";
}