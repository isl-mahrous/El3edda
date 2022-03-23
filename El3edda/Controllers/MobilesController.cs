﻿#nullable disable
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

namespace El3edda.Controllers
{
    public class MobilesController : Controller
    {

        private readonly IMobileService _service;
        private readonly IManufacturerService _serviceMan;

        public MobilesController(IMobileService service, IManufacturerService serviceMan)
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
            if (id == null)
            {
                return NotFound();
            }

            var mobile = await _service.GetByIdAsync(id);

            if (mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        public async Task<IActionResult> ViewSpecs(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var mobile = await _service.GetByIdAsync(id);
            
            if(mobile == null)
            {
                return NotFound();
            }

            return View(mobile);
        }

        // GET: Mobiles/Create
        public IActionResult Create()
        {
            ViewBag.Manufactures = _serviceMan.GetAllAsync();
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Serial,Name,ReleaseDate,Price,Description,WarrantyPeriod,UnitsInStock,UnitsSold")] Mobile mobile)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(mobile);
                return RedirectToAction(nameof(Index));
            }
            return View(mobile);
        }

        // GET: Mobiles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Serial,Name,ReleaseDate,Price,Description,WarrantyPeriod,UnitsInStock,UnitsSold,ManID")]*/ Mobile mobile)
        {
            if (id != mobile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(mobile);
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
            return View(mobile);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
