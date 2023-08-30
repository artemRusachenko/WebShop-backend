using lesson1_Simple_Functions___Controller.Models;

namespace lesson1_Simple_Functions___Controller.Services.UserService
{
    public interface IUserService
    {
        Task<GetUserDto> GetUserInfo();
        Task<GetUserDto?> UpdateUserData (UpdateUserDataDto redactedUser);
        Task<GetUserDto?> UpdateUserPassword(UpdateUserPasswordDto redactedUser);
    }
}
