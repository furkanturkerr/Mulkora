using Otelvexa.DataAccess.Abstract;
using Otelvexa.DataAccess.Concrete;
using Otelvexa.DataAccess.Repository;
using Otelvexa.Entity.Concrete;

namespace Otelvexa.DataAccess.EntityFramework;

public class EfTestimonialDal : GenericRepository<Testimonial>, ITestimonialDal
{
    public EfTestimonialDal(Context context) : base(context)
    {
    }
}