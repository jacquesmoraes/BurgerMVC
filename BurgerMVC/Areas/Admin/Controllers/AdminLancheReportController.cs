using BurgerMVC.Areas.Services;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using LanchesMac.Areas.Admin.FastReport;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLancheReportController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly RelatorioLanchesService _relatorioLancheService;

        public AdminLancheReportController(IWebHostEnvironment environment, RelatorioLanchesService relatorioLancheService)
        {
            _environment = environment;
            _relatorioLancheService= relatorioLancheService;
            
        }

        public async Task<IActionResult> LanchesCategoriaReport()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);
            webReport.Report.Load(Path.Combine
                (_environment.ContentRootPath, "wwwroot/Reports", "LanchesCategoria.frx"));
            var lanches = HelperFastReport.GetTable( await _relatorioLancheService.GetLancheReport(), "LanchesReport");
            var categoria = HelperFastReport.GetTable( await _relatorioLancheService.GetCategoriaReport(), "CategoriaReport");

            webReport.Report.RegisterData(lanches, "LanchesReport");
            webReport.Report.RegisterData(categoria, "CategoriaReport");
            return View(webReport);

        }

        [Route("LanchesCategoriaPdf")]
        public async Task<IActionResult> LanchesCategoriaReportPDF()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);
            webReport.Report.Load(Path.Combine
                (_environment.ContentRootPath, "wwwroot/Reports", "LanchesCategoria.frx"));
            var lanches = HelperFastReport.GetTable(await _relatorioLancheService.GetLancheReport(), "LanchesReport");
            var categoria = HelperFastReport.GetTable(await _relatorioLancheService.GetCategoriaReport(), "CategoriaReport");

            webReport.Report.RegisterData(lanches, "LanchesReport");
            webReport.Report.RegisterData(categoria, "CategoriaReport");
            webReport.Report.Prepare();

            Stream stream = new MemoryStream();
            webReport.Report.Export(new PDFSimpleExport(),stream);
            stream.Position = 0;
            ////download file
            //return File(stream, "", "LancheCategoria.pdf");
            
            //open in browser
            return new FileStreamResult(stream, "application/pdf");


        }
    }
}
