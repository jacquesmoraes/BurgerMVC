using BurgerMVC.Areas.Admin.AdminRepository.AdminInterfaces;
using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BurgerMVC.Areas.Admin.AdminRepository
{

    public class AdminLancheRepository : IAdminLancheRepository
    {
        private readonly AppDbContext _context;
        public AdminLancheRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Lanche> GetLancheById(int id)
        {
            return await _context.Lanches.Include(c => c.Categoria).FirstOrDefaultAsync(x => x.LancheId == id);
        }

        public async Task<List<Lanche>> FindAllLanche()
        {
            return await _context.Lanches.Include(x => x.Categoria).OrderBy(x => x.Nome).ToListAsync();
        }


        public async Task AddLanche(Lanche lanche)
        {
            await _context.Lanches.AddAsync(lanche);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveLanche(int id)
        {
            try
            {
                var obj = await _context.Lanches.FindAsync(id);
                _context.Lanches.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        public async Task UpdateCategoria(Lanche lanche)
        {
            try
            {
                _context.Lanches.Update(lanche);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DBConcurrencyException(ex.Message);
            }
        }

        public bool HasInData(string nome)
        {
            return _context.Lanches.Any(x => x.Nome == nome);
        }


    }
}
