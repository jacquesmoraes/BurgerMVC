using BurgerMVC.Context;
using BurgerMVC.Models;

namespace BurgerMVC.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;
        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra= carrinhoCompra;
        }
     

            public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var PedidoDetail = new PedidoDetalhe
                {
                    PedidoId = pedido.PedidoId,
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    Preco = carrinhoItem.Lanche.Preco
                };
                _context.PedidosDetalhe.Add(PedidoDetail);
            }

            _context.SaveChanges();
        }

    }
}
