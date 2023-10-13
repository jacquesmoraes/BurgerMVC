namespace BurgerMVC.Models.ViewModels
{
    public class PedidoDetalheViewModel
    {
        public Pedido Pedido { get; set; }
        public List<PedidoDetalhe> PedidoDetalhes { get; set; }
       
    }
}
