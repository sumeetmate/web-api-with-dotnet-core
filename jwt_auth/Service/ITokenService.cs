using Microsoft.AspNetCore.Identity;

namespace jwt_auth.Service
{
    public interface ITokenService
    {
        public string CreateToken(IdentityUser user);
    }
}
