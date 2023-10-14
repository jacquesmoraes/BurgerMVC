using BurgerMVC.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BurgerMVC.Dbinitializer;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async void Seed()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
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

         _userManager.CreateAsync(new IdentityUser
         {
            UserName = "jacquesbmoraes@hotmail.com",
            Email = "jacquesbmoraes@hotmail.com",
            EmailConfirmed = true
            
        }, "\"Jacques40707\"").GetAwaiter().GetResult();

        IdentityUser user = await _context.Users.FirstOrDefaultAsync(x => x.Email == "jacquesbmoraes@hotmail.com");
          _userManager.AddToRoleAsync(user, Utilities.Helper.Admin).GetAwaiter().GetResult();
    }

   
}