using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Museo.Services;
using Museo.ViewModels;

namespace Museo.Controllers
{
    public class ProfilesController : Controller
    {

        private readonly IProfilesService _profileService;

        public ProfilesController (IProfilesService profileService)
        {
            _profileService = profileService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> My()
        {
            var u = await _profileService.GetMyProfile(User);
            if (u==null) return NotFound();
            return View(u);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateProfile()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProfile(CreateProfilesViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var success =await _profileService.CreateProfile(User, Model);

            if (!success) return View(Model);

            return RedirectToAction(nameof(My));
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Editprofile()
        {
            var u = await _profileService.GetEditProfile(User);
            if (u == null) return NotFound();
            return View(u);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Editprofile(EditProfilesViewModel Model )
        {
            if (!ModelState.IsValid) return View(Model);
            var success = await _profileService.EditProfile(User, Model);

            if (!success)
                return View(Model);
            return RedirectToAction(nameof(My));

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            var seccess= await _profileService.DeleteProfile(User);

            if (!seccess) return BadRequest();
            return RedirectToAction(nameof(My));
        }
    }
}
