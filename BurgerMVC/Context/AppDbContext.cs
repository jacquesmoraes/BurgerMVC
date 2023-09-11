using BurgerHouse.Models;
using BurgerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMVC.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
