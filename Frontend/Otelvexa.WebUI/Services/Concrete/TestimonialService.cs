using Otelvexa.Dto.TestimonialDtos;
using Otelvexa.WebUI.Services.Abstract;

namespace Otelvexa.WebUI.Services.Concrete;

public class TestimonialService : GenericService<ResultTestimonialDto, CreateTestimonialDto, UpdateTestimonialDto>, ITestimonialService
{
    public TestimonialService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string ApiRoute => "api/Testimonials";
}