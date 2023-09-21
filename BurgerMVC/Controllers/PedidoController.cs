using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            double precoTotalPedido = 0.00 ;

            //obtem itens do carrinho de compras
            List<ItensDoCarrinho> carrinhoItens = _carrinhoCompra.GetItensDoCarrinhos();
            _carrinhoCompra.CarrinhoCompraItens = carrinhoItens;


            //verificar se existe itens no pedido
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "seu carrinho esta vazio");
            }
            //calcula o total de itens e o ttotal do pedido
            foreach (var item in carrinhoItens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);

            }
            //atribui os valores obtidos ao pedido
            pedido.TotalItensDoPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //valida os dados do pedido
            if (ModelState.IsValid)
            {
                //cria pedido e detalhes
                _pedidoRepository.CriarPedido(pedido);
                
                //definir mensagem ao cliente
                ViewBag.CheckoutCompletoMensagem = "obrigado pelo pedido";
                ViewBag.TotalPedido = _carrinhoCompra.Total();
                ViewBag.DataPedido = DateTime.Now;
                //limpa o carrinho
                _carrinhoCompra.RemoveTodos();

                //exibe a view com detalhes do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);

        }
    }
}
