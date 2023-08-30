using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.DTOs.ReviewsDtos;
using lesson1_Simple_Functions___Controller.Models;

namespace lesson1_Simple_Functions___Controller.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;

        public ReviewService(PostgreSqlContext context, IMapper mapper)
        {
            postgreSqlContext = context;
            this.mapper = mapper;
        }

        public async Task<List<GetReviewDto>> GetReviews()
        {
            var reviews = await postgreSqlContext.Reviews.ToListAsync();
            List<GetReviewDto> res = new();
            foreach (var productReview in reviews)
            {
                var User = postgreSqlContext.Users.FirstOrDefault(u => u.Id == productReview.UserId);
                var product = postgreSqlContext.Products.FirstOrDefault(p => p.Id == productReview.ProductId);
                res.Add(new GetReviewDto(productReview.Id, product.Name, productReview.Text, productReview.Rating, productReview.User.Name, productReview.CreatedDate));
            }
            return res;
        }

        public async Task<List<GetReviewDto>> GetProductReviews(int productId)
        {
            var productReviews = await postgreSqlContext.Reviews.Where((r) => r.ProductId == productId).ToListAsync();
            List<GetReviewDto> res = new();
            foreach( var productReview in productReviews)
            {
                var User = postgreSqlContext.Users.FirstOrDefault(u => u.Id == productReview.UserId);
                var product = postgreSqlContext.Products.FirstOrDefault(p => p.Id == productReview.ProductId);
                res.Add(new GetReviewDto(productReview.Id, product.Name, productReview.Text, productReview.Rating, productReview.User.Name, productReview.CreatedDate));
            }
            res.Reverse();
            return res;
        }

        public async Task AddReview(AddReviewDto newReview)
        {
            Review review = new();
            review.Text = newReview.Text;
            review.Rating = newReview.Rating;
            review.User = await postgreSqlContext.Users.FirstOrDefaultAsync(u => u.Id == newReview.UserId);
            review.Product = await postgreSqlContext.Products.FirstOrDefaultAsync(p => p.Id == newReview.ProductId);

            postgreSqlContext.Reviews.Add(review);
            await postgreSqlContext.SaveChangesAsync();
        }

        public async Task<List<GetReviewDto>?> GetUserReviews(int userId)
        {
            var userReviews = await postgreSqlContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
            var user = postgreSqlContext.Users.FirstOrDefault(u => u.Id == userId);
            List<GetReviewDto> res = new();
            foreach (var review in userReviews)
            {
                var product = postgreSqlContext.Products.FirstOrDefault(p => p.Id == review.ProductId);
                res.Add(new GetReviewDto(review.Id, product.Name, review.Text, review.Rating, user.Name, review.CreatedDate));
            }
            res.Reverse();
            return res;
        }
    }
}
