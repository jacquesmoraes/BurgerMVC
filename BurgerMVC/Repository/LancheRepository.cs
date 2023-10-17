using BurgerMVC.Context;
using BurgerMVC.Models;
using BurgerMVC.Repository.Exceptions;
using BurgerMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BurgerMVC.Repository;

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



    public async Task<Lanche> GetLancheById(int id)
    {
         return await _context.Lanches.Include(c => c.Categoria).FirstOrDefaultAsync(x => x.LancheId == id);
    }


}
