using BurgerMVC.Areas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminRelatorioVendasController : Controller
{
    private readonly RelatorioVendasService _relatorioService;

    public AdminRelatorioVendasController(RelatorioVendasService relatorioService)
    {
        _relatorioService= relatorioService;
    }


    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RelatorioSimples(DateTime? minDate, DateTime? maxDate)
    {
        if(!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if(!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
        var result = await _relatorioService.FindByDateAsync(minDate, maxDate);
        return View(result);

    }


}
