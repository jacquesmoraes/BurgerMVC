using BurgerMVC.Areas.Admin.AdminRepository.AdminInterfaces;
using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Data;
using System.Diagnostics;

namespace BurgerMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly IAdminLancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly AppDbContext _context;
        public AdminLanchesController(AppDbContext context, IAdminLancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
            _context = context;
        }


        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Lanches.Include(x => x.Categoria).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(x => x.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var lanche = await _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(lanche);
        }


        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_categoriaRepository.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImgThumbUrl,IsLanchePreferido,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (_lancheRepository.HasInData(lanche.Nome))
            {
                ModelState.AddModelError("", "O lanche ja existe no banco de dados");

            }
            if (ModelState.IsValid)
            {
                await _lancheRepository.AddLanche(lanche);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_categoriaRepository.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var lanche = await _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }
            ViewData["CategoriaId"] = new SelectList(_categoriaRepository.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImgThumbUrl,IsLanchePreferido,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (id != lanche.LancheId)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = "Id not found"
                });
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Nao foi possivel editar");
            }

            else
            {
                try
                {
                    await _lancheRepository.UpdateCategoria(lanche);

                }
                catch (Exception e)
                {
                    RedirectToAction(nameof(e.Message));
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_categoriaRepository.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);
            return View(lanche);
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            var lanche = await _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    Message = "Id not found"
                });
            }

            return View(lanche);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_lancheRepository.FindAllLanche() == null)
            {
                return Problem("Entity set 'AppDbContext.Lanches'  is null.");
            }
            try
            {
                await _lancheRepository.RemoveLanche(id);
                return RedirectToAction(nameof(Index));
            }
             catch(Exception e)
            {
                return RedirectToAction(nameof(e.Message));
            }
        }

        public IActionResult Error(string message)
        {
            var error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message
            };
            return View(error);
        }
    }
}
