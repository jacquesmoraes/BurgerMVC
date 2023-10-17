using BurgerMVC.Models;
using BurgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BurgerMVC.Controllers;

public class AccountController : Controller
{

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
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
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel uservm)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(uservm.UserName, uservm.Password, uservm.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(uservm.UserName);
                HttpContext.Session.SetString("user_id", user.Name );
                _ = HttpContext.Session.GetString("user_id");

                return RedirectToAction("Index", "Home");

            }
        }
        ModelState.AddModelError("", "Nao foi possivel fazer login");

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
    public async Task<IActionResult> Register(RegisterViewModel registerUser)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                Name = registerUser.Nome,
                UserName = registerUser.Email.ToLower(),
                Email = registerUser.Email.ToLower(),

            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");
                await _userManager.AddClaimAsync(user, new Claim("Name", user.Name));
                await _userManager.AddClaimAsync(user, new Claim("Email", user.Email));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Registro", "usuario nao pode ser criado");
            }
        }
        return View(registerUser);

    }

    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

}