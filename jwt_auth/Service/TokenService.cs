using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt_auth.Service
{
    public class TokenService : ITokenService
    {
        private const int ExpirationInMinutes = 30;
        public string CreateToken(IdentityUser user)
        {
            var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), DateTime.Now.AddMinutes(ExpirationInMinutes));
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials signingCredentials, DateTime expiration) =>
        new("jwt_auth_api", "jwt_auth_api", claims, expires: expiration, signingCredentials: signingCredentials);

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!jwt_auth_docker_api#")), SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> CreateClaims(IdentityUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiAuth"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            return claims;
        }
    }
}