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

        public IActionResult Index(specSearchParamter searchParam)
        {
            
            PropSearch searchCriteria = new PropSearch(searchParam);
            //searchCriteria.StringSearch("samsung")
            //              .PriceLowerSearch(1000);
            var data = Context.Mobiles.Where(searchCriteria.searchPredicate).ToList();
            return View(data);
        }
        
        // POST: Manufacturers/Create
        
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(specSearchParamter searchParam)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), searchParam);
            }
            return View(searchParam);
        }
    }
}
