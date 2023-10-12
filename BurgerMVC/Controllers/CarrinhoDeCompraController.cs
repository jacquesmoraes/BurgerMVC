using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Controllers
{
    [Authorize]
    public class CarrinhoDeCompraController : Controller
    {

        private readonly CarrinhoCompra _carrinhoCompras;
        private readonly ILancheRepository _lancheRepository;

        public CarrinhoDeCompraController(CarrinhoCompra carrinhoCompra, ILancheRepository lancheRepository)
        {
            _carrinhoCompras = carrinhoCompra;
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompras.GetItensDoCarrinhos();
            _carrinhoCompras.CarrinhoCompraItens = itens;

            var itensVm = new CarrinhoCompraViewModel
            {
                carrinhoCompra = _carrinhoCompras,
                CarrinhoCompraTotal = _carrinhoCompras.Total()
            };

            return View(itensVm);
        }

        public IActionResult AddItemCarrinho(int id)
        {
            var result = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == id);

            if (result != null)
            {
                _carrinhoCompras.AddItem(result);
            }
            return RedirectToAction(nameof(Index));

        }
        public IActionResult RemoveItemCarrinho(int id)
        {
            var result = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == id);

            if (result != null)
            {

                _carrinhoCompras.RemoverItem(result);
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
