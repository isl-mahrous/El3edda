using El3edda.Data.Services.MobileService;
using El3edda.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

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

        public async Task<IActionResult> Index()
        {
            var mobiles = await service.GetAllAsync();
            var MobilesInStock = mobiles.Where(m => m.UnitsInStock > 0);
            ViewBag.BestDealMobile = MobilesInStock.OrderBy(p => p.Price).First();
            ViewBag.TopSellers = MobilesInStock.OrderByDescending(p => p.UnitsSold).Take(8);
            ViewBag.NewArrivals = MobilesInStock.OrderByDescending(p => p.ReleaseDate).Take(4);
            ViewBag.HotSales = MobilesInStock.OrderBy(p => p.Price).Take(4);
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

        public IActionResult Contacts()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Contacts(string email, string name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("el3edda@gmail.com", "EL-3edda Store");
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "El3edda123";
                    var sub = name;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = "New Year's Sale",
                        Body = $"Hey {name}, Don't miss our latest collections of mobiles. El3edda is rocking it!"
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}