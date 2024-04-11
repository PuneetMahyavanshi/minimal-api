using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Model.Auth;
using MinimalAPI.Repository.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalAPI.Repository
{
    public class CustomTokenHandler : ICustomTokenHandler
    {
        private readonly IConfiguration _configuration;

        public CustomTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserModel user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration.GetValue<string>("Auth:SecretKey");
                var key = Encoding.UTF8.GetBytes(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                             SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                return null;
            }
        }
    }
}
