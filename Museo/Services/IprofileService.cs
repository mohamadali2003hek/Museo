using Museo.ViewModels;
using System.Security.Claims;

namespace Museo.Services
{
    public interface IprofileService
    {
       Task<ProfileViewModel?> GetMyProfile(ClaimsPrincipal user) ;

       Task<bool> EditProfile(ClaimsPrincipal user, EditProfileViewModel model);

       Task<EditProfileViewModel?> GetEditProfile(ClaimsPrincipal user);

       Task<bool> DeleteAccount(ClaimsPrincipal user);
    }
}
