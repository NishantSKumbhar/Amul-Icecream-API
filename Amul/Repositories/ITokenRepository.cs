using Microsoft.AspNetCore.Identity;

namespace Amul.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
