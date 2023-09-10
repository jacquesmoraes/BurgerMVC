using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BurgerHouse.Models;

namespace BurgerMVC.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }
        [Required(ErrorMessage = "O nome do lanche deve ser informado")]
        [Display(Name = "Nome do lanche")]

        public string Nome { get; set; }
        [Required(ErrorMessage = "Uma descrição precisa ser informada")]
        [Display(Name = "Descrição do lanche")]
        [MaxLength(100, ErrorMessage = "Limite de caractes excedido, {1}")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Uma descrição precisa ser informada")]
        [Display(Name = "Descrição do lanche")]
        [MaxLength(200, ErrorMessage = "Limite de caractes excedido, {1}")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "o valor deve estar entre {0} e {1}")]
        public double Preco { get; set; }
        public string ImagemUrl { get; set; }
        public string ImgThumbUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        public Lanche()
        {
        }

        public Lanche(int lancheId, string nome, string descricaoCurta, string descricaoDetalhada, double preco, string imagemUrl, string imgThumbUrl, bool isLanchePreferido, bool emEstoque, Categoria categoria)
        {
            LancheId = lancheId;
            Nome = nome;
            DescricaoCurta = descricaoCurta;
            DescricaoDetalhada = descricaoDetalhada;
            Preco = preco;
            ImagemUrl = imagemUrl;
            ImgThumbUrl = imgThumbUrl;
            IsLanchePreferido = isLanchePreferido;
            EmEstoque = emEstoque;
            Categoria = categoria;
        }


    }
}
