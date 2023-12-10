using Microsoft.AspNetCore.Mvc;
using Projek_SIM.Models;
using System.Diagnostics;

namespace Projek_SIM.Controllers
{
    public class HomeController : Controller
    {
        PolmanAstraLibrary.PolmanAstraLibrary lib = new PolmanAstraLibrary.PolmanAstraLibrary("Server=ASUS;Database=ERP_PoltekAstra;User=NISA;Password=123;");

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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