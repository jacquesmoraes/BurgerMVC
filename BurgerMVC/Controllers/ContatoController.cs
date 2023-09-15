using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
