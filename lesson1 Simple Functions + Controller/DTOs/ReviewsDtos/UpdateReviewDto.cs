namespace lesson1_Simple_Functions___Controller.DTOs.ReviewsDtos
{
    public class UpdateReviewDto
    {
        public string Text { get; set; } = "";
        public int Rating { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
