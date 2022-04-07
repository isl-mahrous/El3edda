using El3edda.Data.Base;
using El3edda.Models;

namespace El3edda.Data.Services.ReviewService
{
    public class ReviewService : EntityBaseRepository<Review>, IReviewService
    {
        public ReviewService(AppDbContext context) : base(context) { }
    }
}
