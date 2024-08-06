using Microsoft.AspNetCore.Identity;

namespace PanteonAdminPanel.API.Repositories
{
    public interface ITokenRepository
    {
        string GenerateToken(IdentityUser user);
    }
}
