using BurgerMVC.Models;

namespace BurgerMVC.Repository
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchePreferido { get;  }

        Lanche GetLancheById(int id);
    }
}
