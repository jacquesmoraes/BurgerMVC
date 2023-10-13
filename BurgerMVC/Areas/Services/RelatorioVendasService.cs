using BurgerMVC.Context;
using BurgerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMVC.Areas.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;
        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            }
            if(maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado<= maxDate.Value);
            }

            return await resultado.Include( x => x.PedidoItens).ThenInclude(x => x.Lanche)
                .OrderByDescending(x => x.PedidoEnviado).
                ToListAsync();
        }
    }
}
