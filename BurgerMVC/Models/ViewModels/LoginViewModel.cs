

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BurgerMVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Informe o nome")]
        [DisplayName("Usuario")]
        public string  UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DisplayName("senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Lembrar")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
