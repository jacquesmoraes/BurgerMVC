﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Repository.Interfaces;
using BurgerMVC.Repository.Exceptions;
using BurgerHouse.Models;

namespace BurgerMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public AdminLanchesController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _lancheRepository.FindAllLanche();
            return View(appDbContext);
        }

     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _lancheRepository.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
            {
                return NotFound();
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

        // GET: Admin/Lanches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _lancheRepository.Lanches == null)
            {
                return NotFound();
            }

            var lanche = await _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
            {
                return NotFound();
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
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                 ModelState.AddModelError("", "Nao foi possivel editar");
            }
          
            else { 
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

        //// GET: Admin/Lanches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var lanche = await _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // POST: Admin/Lanches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_lancheRepository.Lanches == null)
            {
                return Problem("Entity set 'AppDbContext.Lanches'  is null.");
            }
            var lanche = await _lancheRepository.GetLancheById(id);
            if (lanche != null)
            {
                await _lancheRepository.RemoveLanche(id);
            }

            
            return RedirectToAction(nameof(Index));
        }


    }
}