using Microsoft.AspNetCore.Mvc;
using El3edda.Models;
using El3edda.Data;
using El3edda.utills;

namespace El3edda.Controllers
{
    public class FilterController : Controller
    {
        public FilterController(AppDbContext _context)
        {
            Context = _context;
        }

        public AppDbContext Context { get; }

        public IActionResult Index()
        {
            //var mobiles = Context.Mobiles.ToList();
            IPropSearch searchCriteria = new PropSearch();
            //searchCriteria = new StringSearch(searchCriteria, "samsung");
            searchCriteria = new PriceLowerSearch(searchCriteria, 800);
            var data = Context.Mobiles.Where(searchCriteria.searchPredicate).ToList();
            return View(data);
        }
    }
}
