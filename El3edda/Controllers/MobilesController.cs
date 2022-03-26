#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using El3edda.Data;
using El3edda.Models;
using El3edda.Data.Services.MobileService;
using El3edda.Data.Services;
using El3edda.utills;

namespace El3edda.Controllers
{
    public class MobilesController : Controller
    {

        private readonly IMobileService _service;
        private readonly IManufacturerService _serviceMan;
        private readonly AppDbContext _context;

        public MobilesController(IMobileService service, IManufacturerService serviceMan,AppDbContext context)
        {
            _service = service;
            _serviceMan = serviceMan;
            _context = context;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index(specSearchParamter searchParam)
        {
            PropSearch searchCriteria = new PropSearch(searchParam);
            var data = _context.Mobiles.Include(m => m.Manufacturer).Where(searchCriteria.searchPredicate).ToList();
            return View(data);
        }

        
        public async Task<IActionResult> Details(int id)
        {

            var mobile = await _service.GetByIdAsync(id);

            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        public async Task<IActionResult> ViewSpecs(int id)
        {

            var mobile = await _service.GetByIdAsync(id);
            
            if(mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // GET: Mobiles/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Manufactures = await _serviceMan.GetAllAsync();
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Name,Price,Description,UnitsSold,UnitsInStock,ManufacturerId,ReleaseDate,WarrantyPeriod,MainPhotoURL,Specs")] Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(mobile);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Manufactures = await _serviceMan.GetAllAsync();
            return View(mobile);
        }

        // GET: Mobiles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var mobile = await _service.GetByIdAsync(id);
            if (mobile == null)
            {
                return NotFound();
            }

            ViewBag.Manufactures = await _serviceMan.GetAllAsync();

            return View(mobile);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mobile mobile)
        {
            if (id != mobile.Id)
            {
                return NotFound();
            }

            var editedmobile = await _service.GetByIdAsync(id);
            
            editedmobile.Name = mobile.Name;
            editedmobile.Price = mobile.Price;
            editedmobile.Description = mobile.Description;
            editedmobile.UnitsSold = mobile.UnitsSold;
            editedmobile.UnitsInStock = mobile.UnitsInStock;
            editedmobile.ManufacturerId = mobile.ManufacturerId;
            editedmobile.ReleaseDate = mobile.ReleaseDate;
            editedmobile.WarrantyPeriod = mobile.WarrantyPeriod;
            editedmobile.MainPhotoURL = mobile.MainPhotoURL;
            editedmobile.Specs.CPU = mobile.Specs.CPU;
            editedmobile.Specs.OS = mobile.Specs.OS;
            editedmobile.Specs.Color = mobile.Specs.Color;
            editedmobile.Specs.BatteryCapacity = mobile.Specs.BatteryCapacity;
            editedmobile.Specs.Height = mobile.Specs.Height;
            editedmobile.Specs.Width = mobile.Specs.Width;
            editedmobile.Specs.Thickness = mobile.Specs.Thickness;
            editedmobile.Specs.CameraMegaPixels = mobile.Specs.CameraMegaPixels;
            editedmobile.Specs.Screen = mobile.Specs.Screen;
            editedmobile.Specs.Weight = mobile.Specs.Weight;

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, editedmobile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileExists(mobile.Id))
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

            ViewBag.Manufactures = await _serviceMan.GetAllAsync();
            return View(mobile);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var mobile = await _service.GetByIdAsync(id);

            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // POST: Mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MobileExists(int id)
        {
            return _service.GetByIdAsync(id) != null;
        }
    }
}
