using lesson1_Simple_Functions___Controller.DTOs.ReviewsDtos;
using lesson1_Simple_Functions___Controller.Models;
using lesson1_Simple_Functions___Controller.Services.ReviewService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService service) 
        {
            reviewService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetReviewDto>>> GetReviews()
        {
            var result = await reviewService.GetReviews();
            return Ok(result);
            
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<List<GetReviewDto>>> GetProductReviews(int productId)
        {
            var result = await reviewService.GetProductReviews(productId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(AddReviewDto newReview)
        {
            await reviewService.AddReview(newReview);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<GetReviewDto>>> GetUserReviews(int userId)
        {
            var result = await reviewService.GetUserReviews(userId);
            return Ok(result);
        }
    }
}
