using El3edda.Data.Services.MediaService;
using El3edda.Data.Services.MobileService;
using El3edda.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Controllers
{
    public class MediaController : Controller
    {

        private readonly IMediaService _service;
        private readonly IMobileService _serviceMan;

        public MediaController(IMediaService service, IMobileService serviceMan)
        {
            _service = service;
            _serviceMan = serviceMan;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Mobiles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var media = await _service.GetByIdAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        public async Task<IActionResult> ViewSpecs(int id)
        {

            var media = await _service.GetByIdAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: Mobiles/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Mobiles = await _serviceMan.GetAllAsync();
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind(include: "Name,Price,Description,UnitsSold,UnitsInStock,ManufacturerId,ReleaseDate,WarrantyPeriod,MainPhotoURL,Specs")]*/ Media media)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(media);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mobiles = await _serviceMan.GetAllAsync();
            return View(media);
        }

        // GET: Mobiles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var media = await _service.GetByIdAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            ViewBag.Mobiles = await _serviceMan.GetAllAsync();

            return View(media);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Media media)
        {
            if (id != media.Id)
            {
                return NotFound();
            }

            var editedmedia = await _service.GetByIdAsync(id);

            //editedmobile.Name = mobile.Name;
            //editedmobile.Price = mobile.Price;
            //editedmobile.Description = mobile.Description;
            //editedmobile.UnitsSold = mobile.UnitsSold;


            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, editedmedia);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mobiles = await _serviceMan.GetAllAsync();
            return View(media);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var media = await _service.GetByIdAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MediaExists(int id)
        {
            return _service.GetByIdAsync(id) != null;
        }
    }
}
