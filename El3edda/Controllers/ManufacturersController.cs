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
using El3edda.Data.Services;

namespace test.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly IManufacturerService _service;

        public ManufacturersController(IManufacturerService service)
        {
            _service = service;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Manufacturers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
                return NotFound();

            return View(manufacturerDetails);
        }

        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Origin")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(manufacturer);
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
                return NotFound();

            return View(manufacturerDetails);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,Origin")] Manufacturer manufacturer
        )
        {
            if (!ModelState.IsValid)
                return View(manufacturer);

            await _service.UpdateAsync(id, manufacturer);
            return RedirectToAction(nameof(Index));
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
                return NotFound();

            return View(manufacturerDetails);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
