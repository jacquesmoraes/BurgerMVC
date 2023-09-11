﻿
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BurgerMVC.Models.ViewModels;
using BurgerMVC.Repository;

namespace BurgerMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILogger<HomeController> logger, ILancheRepository lancheRepository)
        {
            _logger = logger;
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchePreferido
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}