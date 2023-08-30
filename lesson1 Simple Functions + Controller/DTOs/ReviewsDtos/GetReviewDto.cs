namespace lesson1_Simple_Functions___Controller.DTOs.ReviewsDtos
{
    public class GetReviewDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Text { get; set; } = "";
        public int Rating { get; set; }
        private DateTime CreatedDate { get; set; }
        public string CreationDate { get
            {
                return CreatedDate.Date.ToString().Substring(0, 10);
            }
        }

        public string UserName { get; set; }

        public GetReviewDto(int id, string productName, string text, int rating, string userName, DateTime createdDate)
        {
            Id = id;
            ProductName = productName;
            Text= text;
            Rating= rating;
            UserName= userName;
            CreatedDate = createdDate;
        }
    }
}
