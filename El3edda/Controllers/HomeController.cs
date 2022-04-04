using El3edda.Data.Services.MobileService;
using El3edda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace El3edda.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMobileService service;

        public HomeController(ILogger<HomeController> logger, IMobileService _service)
        {
            _logger = logger;
            service = _service;
        }

        public IActionResult Index()
        {
            var mobiles = service.GetAllAsync().Result;
            ViewBag.TopSellers = mobiles.OrderByDescending(p => p.UnitsSold).Take(3);
            ViewBag.NewArrivals = mobiles.OrderByDescending(p => p.ReleaseDate).Take(3);
            ViewBag.HotSales = mobiles.OrderBy(p => p.Price).Take(3);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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