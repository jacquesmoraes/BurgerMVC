
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerMVC.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        [Required(ErrorMessage ="Nome precisa ser informado")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome precisa ser informado")]
        [StringLength(50)]
        public string  Sobrenome { get; set; }

        [Required(ErrorMessage = "informe seu endereço")]
        [StringLength(100)]
        [DisplayName("endereço")]
        public string  Endereco1 { get; set; }

      
        [StringLength(100)]
        [DisplayName("complemento")]
        public string  Endereco2 { get; set; }

        [Required(ErrorMessage = "informe seu cep")]
        [DisplayName("CEP")]
        [StringLength(10, MinimumLength = 8)]
        public string Cep { get; set; }

        [Required(ErrorMessage ="Informe sua cidade")]
        [StringLength(50)]
        public string  Cidade { get; set; }

        [Required(ErrorMessage = "Informe seu estado")]
        [StringLength(10)]
        public string Estado { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage ="Informe seu telefone")]
        [StringLength(25)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[;]{0,1}\\s*)+$")]
        public string Email { get; set; }

        [Column(TypeName="decimal(18,2)")]
        [ScaffoldColumn(false)]
        [DisplayName("Total do pedido")]
        public double PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Itens no pedido")]
        public int TotalItensDoPedido { get; set; }

        [DisplayName("Data do pedido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data Envio Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEntregue { get; set; }

        public List<PedidoDetalhe> PedidoItens { get; set; } = new List<PedidoDetalhe>();

        public Pedido()
        {
        }

        public Pedido(int pedidoId, string nome, string sobrenome, string endereco1, string endereco2, string cep, string cidade, string estado, string telefone, string email, double pedidoTotal, int totalItensDoPedido, DateTime pedidoEnviado, DateTime pedidoEntregue)
        {
            PedidoId = pedidoId;
            Nome = nome;
            Sobrenome = sobrenome;
            Endereco1 = endereco1;
            Endereco2 = endereco2;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            Telefone = telefone;
            Email = email;
            PedidoTotal = pedidoTotal;
            TotalItensDoPedido = totalItensDoPedido;
            PedidoEnviado = pedidoEnviado;
            PedidoEntregue = pedidoEntregue;
        }
    }
}
