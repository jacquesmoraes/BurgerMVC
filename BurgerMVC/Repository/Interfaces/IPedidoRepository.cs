using BurgerMVC.Models;

namespace BurgerMVC.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
