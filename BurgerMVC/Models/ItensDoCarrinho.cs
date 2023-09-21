using System.ComponentModel.DataAnnotations;

namespace BurgerMVC.Models
{
    public class ItensDoCarrinho
    {
        [Key]
        public int ItensId { get; set; }
        public int Quantidade { get; set; }
        public Lanche  Lanche { get; set; }

        [StringLength(200)]
        public string  CarrinhoDeComprasId { get; set; }
    }
}
