using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Museo.Models;
using Museo.Museo_Database_Context;
using Museo.ViewModels;
using System.Data;
using System.Security.Claims;

namespace Museo.Services
{
    public class ProfilesService:IProfilesService
    {
        private readonly UserManager<Users> _userManager;


        private readonly MuseoDbContext _db ;

        public ProfilesService (UserManager<Users> userManager , MuseoDbContext db )
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ProfilesViewModel?> GetMyProfile(ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return null;

            var profile = await _db.Profiles.SingleOrDefaultAsync(p => p.UserId == u.Id);

            if (profile == null)
                return null;
            return new ProfilesViewModel
            { 
                Title = profile.Title,
                IsPrivate = profile.Is_private,
                Theme = profile.Theme
            };
        }

        public async Task<bool> CreateProfile(ClaimsPrincipal user, CreateProfilesViewModel Model)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;

            var prof = new Profiles
            {
                Title = Model.Title,
                Is_private =Model.IsPrivate,
                Theme=Model.Theme,
                UserId = u.Id,
            };
            var existingProfile = await _db.Profiles
            .FirstOrDefaultAsync(p => p.UserId == u.Id);

            if (existingProfile != null)
                return false;

            await _db.Profiles.AddAsync(prof);
            await _db.SaveChangesAsync();

            return true;

        }

        public async Task<EditProfilesViewModel?> GetEditProfile (ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return null;
            var prof= await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == u.Id);
            if (prof == null) return null;
            return new EditProfilesViewModel
            {
                Title = prof.Title,
                Theme = prof.Theme,
                IsPrivate = prof.Is_private
            };



        }

        public async Task<bool> EditProfile(ClaimsPrincipal user, EditProfilesViewModel Model)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            var prof = await _db.Profiles.FirstOrDefaultAsync(p => p.UserId == u.Id);

            if (prof == null)
                return false; 
            if (Model.Title!=null) prof.Title = Model.Title;
            if (Model.Theme != null)  prof.Theme = Model.Theme;
             prof.Is_private = Model.IsPrivate;

            await _db.SaveChangesAsync();
            return true;


        }

        public async Task<bool> DeleteProfile(ClaimsPrincipal user)
        {
            var u = await _userManager.GetUserAsync(user);
            if (u == null) return false;
            var affectedRows = await _db.Profiles
                            .Where(p => p.UserId == u.Id)
                            .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}
