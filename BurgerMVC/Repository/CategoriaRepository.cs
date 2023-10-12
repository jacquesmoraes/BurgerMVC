using BurgerHouse.Models;
using BurgerMVC.Context;
using BurgerMVC.Repository.Exceptions;
using BurgerMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BurgerMVC.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Categorias => _context.Categorias.ToList();

    public async Task<List<Categoria>> FindAll()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria> FindById(int id)
    {
        return await _context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);
    }

    public async Task AddCategoria(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCategoria(Categoria categoria)
    {
        try
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DBConcurrencyException(ex.Message);
        }
    }

    public async Task RemoveCategoria(int id)
    {
        try
        {
            var obj = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(obj);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new IntegrityException(ex.Message);
        }
    }

    public bool HasInData(string nome)
    {
        return _context.Categorias.Any(x => x.CategoriaNome.Equals(nome));
    }


}

