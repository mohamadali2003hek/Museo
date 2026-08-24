
using Microsoft.AspNetCore.Mvc;
using Museo.DTO;

using Museo.Services;
using Museo.ViewModels;

namespace Museo.Controllers
{
   
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel U)
        {
            Console.WriteLine("CONTROLLER REGISTER");

            Console.WriteLine("ModelState: " + ModelState.IsValid);

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    foreach (var e in error.Value.Errors)
                    {
                        Console.WriteLine(e.ErrorMessage);
                    }
                }

                return View(U);
            }

            var dto = new RegisterDto
            {
                Username = U.Username,
                Email = U.Email,
                Password = U.Password,
                ConfirmPassword = U.ConfirmPassword
            };

            var result = await _authService.Register(dto);

            Console.WriteLine("Identity Result: " + result.Succeeded);

            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View(U);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel  model )
        {

            if (!ModelState.IsValid)
                return View(model);

            var dto = new LoginDto
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe
            };


            var result = await _authService.Login(dto);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return RedirectToAction("Login");
        }
    }
}
