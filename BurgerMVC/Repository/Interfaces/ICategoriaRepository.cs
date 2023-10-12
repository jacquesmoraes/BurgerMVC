using BurgerHouse.Models;

namespace BurgerMVC.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
        public Task<List<Categoria>> FindAll();
        Task<Categoria> FindById(int id);
        public bool HasInData(string nome);
        public Task AddCategoria(Categoria categoria);
        public Task RemoveCategoria(int id);
        public Task UpdateCategoria(Categoria categoria);

    }
}
