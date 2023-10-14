using BurgerHouse.Models;
using BurgerMVC.Context;
using BurgerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMVC.Areas.Services
{
    public class RelatorioLanchesService
    {
        private readonly AppDbContext _appDbContext;
        public RelatorioLanchesService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Lanche>> GetLancheReport()
        {
            var list= await _appDbContext.Lanches.ToListAsync();

            if(list is null)
            {
                return default;
            }
            return list;

        }
        public async Task<IEnumerable<Categoria>> GetCategoriaReport()
        {
            var list = await _appDbContext.Categorias.ToListAsync();

            if (list is null)
            {
                return default;
            }
            return list;

        }
    }
}
