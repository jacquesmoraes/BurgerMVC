using BurgerMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace BurgerHouse.Models
{
    public class Categoria
    {

        [Key]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "informe o nome da categoria")]
        [Display(Name = "Categoria")]
        [MaxLength(15, ErrorMessage = "limitte de caracteres excedido, max {1}")]
        public string CategoriaNome { get; set; }

        [Required(ErrorMessage = "informe a descrição da categoria")]
        [Display(Name = "Descrição")]
        [MaxLength(200, ErrorMessage = "Limite de caractes excedido, {1}")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; } = new List<Lanche>();

        public Categoria()
        {
        }

        public Categoria(int categoriaId, string categoriaNome, string descricao)
        {
            CategoriaId = categoriaId;
            CategoriaNome = categoriaNome;
            Descricao = descricao;
        }
    }
}
