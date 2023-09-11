using BurgerHouse.Models;

namespace BurgerMVC.Repository
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get;  }
    }
}
