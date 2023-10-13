using BurgerHouse.Models;

namespace BurgerMVC.Models.ViewModels
{
    public class LanchesViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }

        public string NovaCategoria { get; set; } 

    }
}
