using BurgerMVC.Models;

namespace BurgerMVC.Areas.Admin.AdminRepository.AdminInterfaces
{
    public interface IAdminLancheRepository
    {
        public bool HasInData(string nome);
        Task<Lanche> GetLancheById(int id);
        Task AddLanche(Lanche lanche);
        Task<List<Lanche>> FindAllLanche();
        Task RemoveLanche(int id);
        Task UpdateCategoria(Lanche lanche);
    }
}
