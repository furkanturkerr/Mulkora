using Mulkora.Dto.TestimonialDtos;
using Mulkora.WebUI.Services.Abstract;

namespace Mulkora.WebUI.Services.Concrete;

public class TestimonialService : GenericService<ResultTestimonialDto, CreateTestimonialDto, UpdateTestimonialDto>, ITestimonialService
{
    public TestimonialService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Testimonials";
}