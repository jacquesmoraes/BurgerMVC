using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BurgerMVC.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter no minimo {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [DisplayName("Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas são diferentes")]
        public string ConfirmPassowrd { get; set; }

    }
}
