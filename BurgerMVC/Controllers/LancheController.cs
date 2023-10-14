using BurgerMVC.Models;
using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository.Interfaces;
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
                Lanches = lanches,
                NovaCategoria = novaCategoria
            };
            return View(lanchesListVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var lancheDet = await _lancheRepository.GetLancheById(id);
            return View(lancheDet);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);
                categoriaAtual = "todos os lanches";
            }

            else
            {
                lanches = _lancheRepository.Lanches.Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));
                if (lanches.Any())
                {
                    categoriaAtual = "lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }
            }
            return View("~/Views/Lanche/List.cshtml", new LanchesViewModel
            {
                Lanches = lanches,
                NovaCategoria = categoriaAtual
            });

        }

    }
}
