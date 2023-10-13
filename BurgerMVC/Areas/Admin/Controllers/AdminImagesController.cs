using BurgerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace BurgerMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagesController : Controller
    {
        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminImagesController(IOptions<ConfigurationImages> myConfig, IWebHostEnvironment webHostEnvironment)
        {
            _myConfig = myConfig.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["erro"] = "Arquivo não selecionado";
                return View(ViewData);
            }
            if (files.Count > 10)
            {
                ViewData["Erro"] = "A quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(x => x.Length);
            var pathName = new List<string>();
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") || formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    pathName.Add(fileNameWithPath);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados, " + $"com o tamanho de {size} bytes";
            ViewBag.Arquivos = pathName;

            return View(ViewData);


        }

        public IActionResult GetImages()
        {
            FileManagerModel model = new FileManagerModel();
            var userImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImageProduto = _myConfig.NomePastaImagensProdutos;
            if (files.Length > 0)
            {
                ViewData["Erro"] = $"nenhum arquivo selecionado {userImagesPath}";
            }
            model.Files = files;
            return View(model);

        }
        public IActionResult DeleteFiles(string fileName)
        {
            string _imageDelete = Path.Combine(_webHostEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos + "\\",fileName);
            if(System.IO.File.Exists(_imageDelete))
            {
                System.IO.File.Delete(_imageDelete);
                ViewData["Deletado"] = $"Arquivo {_imageDelete} deletado com sucesso";

            }
            return View("Index");
            
        }

    }
}
