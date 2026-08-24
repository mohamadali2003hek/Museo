using Microsoft.AspNetCore.Identity;
using Museo.DTO;

namespace Museo.Services
{
    public interface IAuthService
    {

        Task<IdentityResult> Register(RegisterDto U);
        Task<SignInResult> Login(LoginDto dto);

        Task Logout();
    }
}
