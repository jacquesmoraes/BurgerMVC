using BurgerMVC.Models;

namespace BurgerMVC.Repository.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchePreferido { get; }
        public bool HasInData(string nome);
        Task<Lanche> GetLancheById(int id);
        Task AddLanche(Lanche lanche);
        Task<List<Lanche>> FindAllLanche();
        Task RemoveLanche(int id);
        Task UpdateCategoria(Lanche lanche);
    }
}
