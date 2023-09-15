using BurgerMVC.Models;
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
        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string novaCategoria = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.Nome).ToList();
                novaCategoria = "Todos os lanches";
            }
            else

            {

                 lanches = _lancheRepository.Lanches.Where(x => x.Categoria.CategoriaNome.Equals(categoria)).OrderBy(x => x.Nome);
                novaCategoria = categoria;
            }
          
                
            
            var lanchesListVM = new LanchesViewModel
            {
                lanches = lanches,
                NovaCategoria = novaCategoria
            };
            return View(lanchesListVM);
        }

        public IActionResult Details(int id)
        {
            var lancheDet = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId== id);
            return View(lancheDet);
        }
    }
}
