namespace lesson1_Simple_Functions___Controller.Services.AuthService
{
    public interface IAuthService
    {
        Task<GetTokenDto> SignUp(SignUpUserDto user);
        Task<GetTokenDto> SignIn(SignInUserDto user);
    }
}
