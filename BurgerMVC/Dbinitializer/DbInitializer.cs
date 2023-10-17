using BurgerMVC.Context;
using BurgerMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BurgerMVC.Dbinitializer;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async void Seed()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }

        if (_context.Roles.Any(x => x.Name == Utilities.Helper.Admin))
        {
            return;
        }

        _roleManager.CreateAsync(new IdentityRole(Utilities.Helper.Admin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(Utilities.Helper.Member)).GetAwaiter().GetResult();

         _userManager.CreateAsync(new ApplicationUser
         {
             
            UserName = "jacquesbmoraes@hotmail.com",
            Email = "jacquesbmoraes@hotmail.com",
            EmailConfirmed = true,
             Name = "Jacques"

         }, "\"Jacques40707\"").GetAwaiter().GetResult();

        ApplicationUser user =  _context.Users.FirstOrDefault(x => x.Email == "jacquesbmoraes@hotmail.com");

        _userManager.AddToRoleAsync(user, Utilities.Helper.Admin).GetAwaiter().GetResult();
        
    }

   
}