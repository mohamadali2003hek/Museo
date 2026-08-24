using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Museo.Models;
using Museo.Services;
using Museo.ViewModels;

namespace Museo.Controllers
{
    public class ProfileController : Controller
    {

        private readonly SignInManager<Users> _signInManager;
        private readonly IprofileService _profileService;

        public ProfileController(
        IprofileService profileService,
        SignInManager<Users> signInManager)
        {
            _profileService = profileService;
            _signInManager = signInManager;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var model = await _profileService.GetEditProfile(User);

            if (model == null)
                return NotFound();

            return View(model);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var u = await _profileService.GetMyProfile(User);
            return View(u);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var success = await _profileService.EditProfile(User, model);

            if (!success)
                return View(model);

            return RedirectToAction("Profile");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var u = await _profileService.DeleteAccount(User);
            if (!u)
                return BadRequest("Could not delete account");

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Auth");
        }
    }
}
