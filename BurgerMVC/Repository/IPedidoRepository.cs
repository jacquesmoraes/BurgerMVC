using BurgerMVC.Models;

namespace BurgerMVC.Repository
{
    public interface IPedidoRepository
    {
         void CriarPedido(Pedido pedido);
    }
}
