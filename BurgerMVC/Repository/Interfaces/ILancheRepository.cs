using BurgerMVC.Models;

namespace BurgerMVC.Repository.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchePreferido { get; }

        Task<Lanche> GetLancheById(int id);
    }
}
