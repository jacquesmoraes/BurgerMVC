using BurgerMVC.Context;
using BurgerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMVC.Repository
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(x => x.Categoria);

        public IEnumerable<Lanche> LanchePreferido => _context.Lanches.
                                    Where(x => x.IsLanchePreferido).
                                    Include(x => x.Categoria);

        public Lanche GetLancheById(int id)
        {
            return _context.Lanches.FirstOrDefault(x => x.LancheId== id);
        }
    }
}
