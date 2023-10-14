using BurgerMVC.Areas.Services;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService _graficoVendasService;

        public AdminGraficoController(GraficoVendasService graficoVendasService)
        {
            _graficoVendasService = graficoVendasService ??
                throw new ArgumentNullException(nameof(graficoVendasService));
        }

        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendasTotais = _graficoVendasService.GetVendasLanche(dias);
            return Json(lanchesVendasTotais) ;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult VendasMensal()
        {
            return View();
        }
        [HttpGet]
        public IActionResult VendasSemanal()
        {
            return View();
        }
    }
}
