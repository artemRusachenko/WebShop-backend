using lesson1_Simple_Functions___Controller.DTOs.ReviewsDtos;

namespace lesson1_Simple_Functions___Controller.Services.ReviewService
{
    public interface IReviewService
    {
        Task<List<GetReviewDto>> GetReviews();
        Task<List<GetReviewDto>> GetProductReviews(int productId);
        Task AddReview(AddReviewDto newReview);
        Task<List<GetReviewDto>> GetUserReviews(int userId);
    }
}
