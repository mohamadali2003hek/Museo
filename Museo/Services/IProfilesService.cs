using Museo.ViewModels;
using System.Security.Claims;

namespace Museo.Services
{
    public interface IProfilesService
    {
        Task<ProfilesViewModel?> GetMyProfile(ClaimsPrincipal user);

        Task<bool> CreateProfile(ClaimsPrincipal user, CreateProfilesViewModel Model);

        Task<EditProfilesViewModel?> GetEditProfile(ClaimsPrincipal user);

        Task<bool> EditProfile(ClaimsPrincipal user, EditProfilesViewModel Model);

        Task<bool> DeleteProfile(ClaimsPrincipal user);
    }
}
