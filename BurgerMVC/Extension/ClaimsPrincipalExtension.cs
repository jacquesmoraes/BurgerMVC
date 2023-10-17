using System.Security.Claims;

namespace BurgerMVC.Extension
{
    public static class ClaimsPrincipalExtension
    {

        public static string GetName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == "Name");
            return firstName?.Value;
        }

        public static string Email(this ClaimsPrincipal principal)
        {
            var email = principal.Claims.FirstOrDefault(c => c.Type == "Email");
            return email?.Value;
        }

     
    }
}
