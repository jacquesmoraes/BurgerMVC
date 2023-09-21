using BurgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [ActionName("Login")]
        public IActionResult LoginGet(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            }); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel uservm)
        {
            if (!ModelState.IsValid)
            {
                return View(uservm);
            }
            var user = await _userManager.FindByNameAsync(uservm.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, uservm.Password, false, false);
                if (result.Succeeded)
                {
                    if(string.IsNullOrEmpty(uservm.ReturnUrl))
                    {
                        return RedirectToAction(nameof(Index), "Home");

                    }
                    return Redirect(uservm.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "falha ao realizar login");
            return View(uservm);
        }

        [HttpGet]
        [ActionName("Register")]
        public IActionResult RegisterGet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registerUser)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser{ UserName = registerUser.UserName };
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    ModelState.AddModelError("Registro", "usuario nao pode ser criado");
                }
            }
            return View(registerUser);
        }

    }
}
