using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lesson1_Simple_Functions___Controller.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly PostgreSqlContext sqlServerContext;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(PostgreSqlContext sqlServerContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.sqlServerContext = sqlServerContext;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetUserDto> GetUserInfo()
        {
            if (httpContextAccessor.HttpContext is not null)
            {
                var result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var user = sqlServerContext.Users.FirstOrDefault(u => u.Email == result);
                if (user == null)
                {
                    return null;
                }
                return mapper.Map<GetUserDto>(user);
            }
            return null;
        }

        public async Task<GetUserDto?> UpdateUserData(UpdateUserDataDto redactedUser)
        {
            var user = await sqlServerContext.Users.FindAsync(redactedUser.Id);
            if (user == null)
                return null;
            user.Name = redactedUser.Name == "" ? user.Name : redactedUser.Name;
            user.Surname = redactedUser.Surname == "" ? user.Surname : redactedUser.Surname;
            user.Patronymic = redactedUser.Patronymic == "" ? user.Patronymic : redactedUser.Patronymic;
            user.PhoneNumber = redactedUser.PhoneNumber == "" ? user.PhoneNumber : redactedUser.PhoneNumber;

            await sqlServerContext.SaveChangesAsync();
            return mapper.Map<GetUserDto>(user);
        }

        public async Task<GetUserDto?> UpdateUserPassword(UpdateUserPasswordDto redactedUser)
        {
            var user = await sqlServerContext.Users.FindAsync(redactedUser.Id);
            if (user == null)
                return null;
            user.Password = redactedUser.Password == "" ? user.Password : BCrypt.Net.BCrypt.HashPassword(redactedUser.Password);
            await sqlServerContext.SaveChangesAsync();
            return mapper.Map<GetUserDto>(user);
        }

        /*private int GetUserIdByToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var res = jwtToken.Claims.First(c => c.Type == "userId").Value;
            return int.Parse(res);
        }*/
    }
}
