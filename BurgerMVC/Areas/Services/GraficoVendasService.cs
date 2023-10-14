using BurgerMVC.Context;
using BurgerMVC.Models;

namespace BurgerMVC.Areas.Services;

public class GraficoVendasService
{
    private readonly AppDbContext _context;

    public GraficoVendasService(AppDbContext context)
    {
        _context = context;
    }

    public List<LanchesGrafico> GetVendasLanche(int dias = 360)
    {
        var data = DateTime.Now.AddDays(-dias);
        var lanches = (from pd in _context.PedidosDetalhe
                       join l
                       in _context.Lanches on pd.LancheId equals l.LancheId
                       where pd.Pedido.PedidoEnviado >= data
                       group pd by new { pd.LancheId, l.Nome }
                       into g
                       select new
                       {
                           NomeLanche = g.Key.Nome,
                           LancheQuantidade = g.Sum(q => q.Quantidade),
                           LancheValorTotal = g.Sum(q => q.Preco * q.Quantidade)
                       });
        var list = new List<LanchesGrafico>();
        foreach (var item in lanches)
        {
            var lanche = new LanchesGrafico();
            lanche.NomeLanche = item.NomeLanche;
            lanche.Quantidade = item.LancheQuantidade;
            lanche.LanchesValorTotal = item.LancheValorTotal;
            list.Add(lanche);
        }
        return list;

    }


}
