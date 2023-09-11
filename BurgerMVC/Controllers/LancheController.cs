using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }
        public IActionResult List()
        {
            //var lanches = _lancheRepository.Lanches;
            var lanchesvm = new LanchesViewModel{
                NovaCategoria = "Categoria Atual",
                lanches = _lancheRepository.Lanches
            };
            
            return View(lanchesvm);
        }
    }
}
