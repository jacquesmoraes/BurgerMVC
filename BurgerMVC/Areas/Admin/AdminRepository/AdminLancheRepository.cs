using BurgerMVC.Context;

namespace BurgerMVC.Areas.Admin.AdminRepository
{
    public class AdminLancheRepository
    {
        private readonly AppDbContext _context;
        public AdminLancheRepository(AppDbContext context)
        {
            _context = context;
        }

        
    }
}
