using BurgerMVC.Models;
using BurgerMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompras;
        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompras = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
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
    }
}
