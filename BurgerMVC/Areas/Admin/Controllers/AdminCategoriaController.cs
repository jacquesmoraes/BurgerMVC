using BurgerHouse.Models;
using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BurgerMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize("Admin")]

public class AdminCategoriaController : Controller
{
    private readonly ICategoriaRepository _categoriaRepository;
    public AdminCategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository= categoriaRepository;
    }
    public async Task<IActionResult> Index()
    {
        var list = await _categoriaRepository.FindAll();
        return View(list);
    }

    public async Task<IActionResult> Details(int? id)
    {
        var obj = await _categoriaRepository.FindById(id.Value);
        return View(obj);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Nao foi possivel cirar");
           
        }
        if (_categoriaRepository.HasInData(categoria.CategoriaNome))
        {
            ModelState.AddModelError("", "A categoria ja existe no banco de dados");
            return View("Edit", categoria);
        }
      
        else
        {
            await _categoriaRepository.AddCategoria(categoria);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var obj = await _categoriaRepository.FindById(id);
        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int CategoriaId)
    {
        try
        {
            await _categoriaRepository.RemoveCategoria(CategoriaId);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(Error), new { ex.Message });
        }
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not Provided" });
        }

        var obj = await _categoriaRepository.FindById(id.Value);
        return View(obj);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPost(Categoria categoria)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Nao foi possivel editar categoria");
            return View(categoria);
        }

        
            try
            {

                await _categoriaRepository.UpdateCategoria(categoria);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Error), new { ex.Message });
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
