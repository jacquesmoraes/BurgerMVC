using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BurgerMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

    }
}
