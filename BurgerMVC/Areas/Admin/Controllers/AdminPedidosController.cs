using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Data;

namespace BurgerMVC.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminPedidosController : Controller
{
    private readonly AppDbContext _context;

    public AdminPedidosController(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
    {
        var resultado = _context.Pedidos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            resultado = resultado.Where(p => p.Nome.Contains(filter));
        }

        var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };

        return View(model);
    }


    // GET: Admin/AdminPedidos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pedido = await _context.Pedidos
            .FirstOrDefaultAsync(m => m.PedidoId == id);

        if (pedido == null)
        {
            return NotFound();
        }

        return View(pedido);
    }



    // GET: Admin/AdminPedidos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        return View(pedido);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PedidoId,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] Pedido pedido)
    {
        if (id != pedido.PedidoId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(pedido.PedidoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(pedido);
    }

    // GET: Admin/AdminPedidos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pedido = await _context.Pedidos
            .FirstOrDefaultAsync(m => m.PedidoId == id);
        if (pedido == null)
        {
            return NotFound();
        }

        return View(pedido);
    }

    // POST: Admin/AdminPedidos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PedidoExists(int id)
    {
        return _context.Pedidos.Any(e => e.PedidoId == id);
    }

    public IActionResult PedidoDetalhe(int id)
    {
        var pedidos = _context.Pedidos.Include(x => x.PedidoItens).
                        ThenInclude(x => x.Lanche).
                        FirstOrDefault(x => x.PedidoId == id);
        if(pedidos == null)
        {
            Response.StatusCode = 404;
            return View("PedidoNotFound", id);
        }
        PedidoDetalheViewModel pedidoVM = new(){
            Pedido = pedidos,
            PedidoDetalhes = pedidos.PedidoItens
            
        };

        return View(pedidoVM);

    }



}