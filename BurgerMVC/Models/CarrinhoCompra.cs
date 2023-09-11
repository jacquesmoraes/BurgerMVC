using BurgerMVC.Context;
using BurgerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMVC.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;
        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoDeComprasId { get; set; }
        public List<ItensDoCarrinho> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinhoCompra(IServiceProvider services)
        {

            // DEFINE UMA SESSÃO
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            //OBTEM UM SERVIÇO DO TIPO DO NOSSO CONTEXT
            var context = services.GetService<AppDbContext>();

            // OBTEM OU GERA UM ID DO CARRINHO

            string carrinhoid = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //ATRUBUI O ID DO CARRINHO NA SESSAO
            session.SetString("CarrinhoId", carrinhoid);

            //RETORNA O CARRINHO COM O CONTEXTO E O ID ATRIBUIDO OU OBTIDO

            return new CarrinhoCompra(context) { CarrinhoDeComprasId = carrinhoid };

        }


        public  List<ItensDoCarrinho> GetItensDoCarrinhos()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoComprasItens.
                                         Where(x => x.CarrinhoDeComprasId == CarrinhoDeComprasId).
                                         Include(x => x.Lanche).ToList());
        }
      


        public void AddItem(Lanche lanche)
        {
            var carrinhoItens = _context.CarrinhoComprasItens .SingleOrDefault(
            x => x.Lanche.LancheId == lanche.LancheId &&
                     x.CarrinhoDeComprasId == CarrinhoDeComprasId
                );

            if (carrinhoItens == null)
            {
                carrinhoItens = new ItensDoCarrinho
                {
                    CarrinhoDeComprasId = CarrinhoDeComprasId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                
                _context.CarrinhoComprasItens.Add(carrinhoItens);
            }

            else
            {
                carrinhoItens.Quantidade++;
            }
            _context.SaveChanges();
        }

        public void RemoverItem(Lanche lanche)
        {
            var carrinhoItens = _context.CarrinhoComprasItens.SingleOrDefault(
            x => x.Lanche.LancheId == lanche.LancheId &&
                     x.CarrinhoDeComprasId == CarrinhoDeComprasId
                );

            if (carrinhoItens != null)
            {
                if (carrinhoItens.Quantidade > 1)
                {
                    carrinhoItens.Quantidade--;
                }
                else
                {
                    _context.CarrinhoComprasItens.Remove(carrinhoItens);
                }

                _context.SaveChanges();
            }

        }

        public void RemoveTodos()
        {
            var carlimpo = _context.CarrinhoComprasItens.Where(x => x.CarrinhoDeComprasId == CarrinhoDeComprasId);
            _context.RemoveRange(carlimpo);
            _context.SaveChanges();
        }

        public double Total()
        {
            var total = _context.CarrinhoComprasItens.Where(x => x.CarrinhoDeComprasId == CarrinhoDeComprasId).
                                       Select(x => x.Lanche.Preco * x.Quantidade).Sum();
            return total;
        }


    }


}


    


