using BurgerHouse.Models;

namespace BurgerMVC.Models.ViewModels
{
    public class LanchesViewModel
    {
        public IEnumerable<Lanche> lanches { get; set; }

        public string NovaCategoria { get; set; } 
    }
}
