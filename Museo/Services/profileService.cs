using Microsoft.AspNetCore.Identity;
using Museo.Models;
using Museo.ViewModels;
using System.ComponentModel;
using System.Security.Claims;

namespace Museo.Services
{
    public class profileService : IprofileService
    {

        private readonly UserManager<Users> _userManager;

        public profileService(UserManager<Users> UserManager)
        {
            _userManager = UserManager;
        }

        public async Task<ProfileViewModel?> GetMyProfile(ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return null;

            return new ProfileViewModel
            {
                Username = u.UserName,
                Email = u.Email,
                Bio = u.Bio,
                CreatedAt = u.CreatedAt
            };
        }

        public async Task<bool> EditProfile(ClaimsPrincipal user, EditProfileViewModel model)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;

            u.Bio = model.Bio;

            var result = await _userManager.UpdateAsync(u);

            return result.Succeeded;
        }


        public async Task<EditProfileViewModel?> GetEditProfile(ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);

            if (u == null)
                return null;

            return new EditProfileViewModel
            {
                Bio = u.Bio
            };
        }



        public async Task<bool> DeleteAccount(ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            var result = await _userManager.DeleteAsync(u);
            return result.Succeeded;
        }


    }
}
