using AutoMapper;
using lesson1_Simple_Functions___Controller.Dataa;
using lesson1_Simple_Functions___Controller.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lesson1_Simple_Functions___Controller.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly PostgreSqlContext sqlServerContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthService(PostgreSqlContext context, IMapper mapper, IConfiguration configuration)
        {
            sqlServerContext = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<GetTokenDto> SignUp(SignUpUserDto newUser)
        {
            var signedInUser = sqlServerContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
            if (signedInUser != null)
                throw new UserException("This email is already signed up", newUser.Email);
            string password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Password = password;

            sqlServerContext.Users.Add(mapper.Map<User>(newUser));
            await sqlServerContext.SaveChangesAsync();

            var user = sqlServerContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
            string token = CreateToken(user);

            GetTokenDto result = new(user.Id, token);
            return result;  
        }

        public async Task<GetTokenDto> SignIn(SignInUserDto user)
        {
            var loggedInUser = sqlServerContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (loggedInUser == null)
                throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, loggedInUser.Password))
                throw new UserException("Wrong Credentials", loggedInUser.Password);

            string token = CreateToken(loggedInUser);

            GetTokenDto result = new(loggedInUser.Id, token);
            return result;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
