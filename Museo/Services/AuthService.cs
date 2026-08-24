using Microsoft.AspNetCore.Identity;
using Museo.DTO;
using Museo.Models;

namespace Museo.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public AuthService(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public async Task<SignInResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(
                user.UserName!,
                dto.Password,
                dto.RememberMe,
                false);

        }

        public async Task<IdentityResult> Register(RegisterDto dto)
        {
            var user = new Users
            {
                UserName = dto.Username,
                Email = dto.Email,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }

            return result;

        }


        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
