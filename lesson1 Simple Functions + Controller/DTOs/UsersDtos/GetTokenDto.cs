namespace lesson1_Simple_Functions___Controller.DTOs.UsersDtos
{
    public class GetTokenDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }

        public GetTokenDto(int userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
