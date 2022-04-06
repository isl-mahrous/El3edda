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

        public IActionResult Index()
        {
            var mobiles = service.GetAllAsync().Result;
            ViewBag.BestDealMobile = mobiles.OrderBy(p => p.Price).First();
            ViewBag.TopSellers = mobiles.OrderByDescending(p => p.UnitsSold).Take(8);
            ViewBag.NewArrivals = mobiles.OrderByDescending(p => p.ReleaseDate).Take(4);
            ViewBag.HotSales = mobiles.OrderBy(p => p.Price).Take(4);
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
                    var senderEmail = new MailAddress("TYPE YOUR MAIL HERE", "gmail");
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "TYPE YOUR PASSWORD HERE";
                    var sub = name;
                    //var body = msg;
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
                        Subject = "El-3edda Store",
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