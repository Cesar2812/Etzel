using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Conocenos()
        {
            return View();
        }

        public IActionResult Valores()
        {
            return View();
        }

        public IActionResult Servicios()
        {
            return View();
        }

        
    }
}
