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
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using El3edda.Data.Enums;
using El3edda.Data.Services.MediaService;

namespace El3edda.Controllers
{

    public class MobilesController : Controller
    {

        private readonly IMobileService _service;
        private readonly IManufacturerService _serviceMan;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMediaService _mediaService;
        public MobilesController(IMediaService mediaService, IMobileService service, IManufacturerService serviceMan,AppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            _serviceMan = serviceMan;
            _context = context;
            _hostEnvironment = hostingEnvironment;
            _mediaService = mediaService;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index(specSearchParamter searchParam)
        {
            PropSearch searchCriteria = new PropSearch(searchParam);
            var data = await _context.Mobiles.Include(m => m.Manufacturer).Where(searchCriteria.searchPredicate).ToListAsync();
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
            
            return View(new MobileViewModel());
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MobileViewModel mobileVM)
        {
            if (ModelState.IsValid)
            {

                string UniqueFileName = "";
                string UniqueFileNameMed = "";
                string FilePath = "";


                if (mobileVM.MainPhotoURL != null)
                {
                    string UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                    UniqueFileName  = Guid.NewGuid().ToString() + "_" + mobileVM.MainPhotoURL.FileName;
                    FilePath = Path.Combine(UploadsFolder, UniqueFileName);
                    mobileVM.MainPhotoURL.CopyTo(new FileStream(FilePath, FileMode.Create));
                }

                Mobile CreatedMobile = new Mobile();


                CreatedMobile.MainPhotoURL = $"/Images/{UniqueFileName}";
                CreatedMobile.Name = mobileVM.Name;
                CreatedMobile.Price = mobileVM.Price;
                CreatedMobile.WarrantyPeriod = mobileVM.WarrantyPeriod;
                CreatedMobile.ManufacturerId = mobileVM.ManufacturerId;
                CreatedMobile.ReleaseDate = mobileVM.ReleaseDate;
                CreatedMobile.UnitsSold = 0;
                CreatedMobile.UnitsInStock = mobileVM.UnitsInStock;
                CreatedMobile.Description = mobileVM.Description;

                CreatedMobile.Specs = new Specs();

                CreatedMobile.Specs.BatteryCapacity = mobileVM.Specs.BatteryCapacity;
                CreatedMobile.Specs.Color = mobileVM.Specs.Color;
                CreatedMobile.Specs.CameraMegaPixels = mobileVM.Specs.CameraMegaPixels;
                CreatedMobile.Specs.CPU = mobileVM.Specs.CPU;
                CreatedMobile.Specs.OS = mobileVM.Specs.OS;
                CreatedMobile.Specs.Screen = mobileVM.Specs.Screen;
                CreatedMobile.Specs.Height = mobileVM.Specs.Height;
                CreatedMobile.Specs.Width = mobileVM.Specs.Width;
                CreatedMobile.Specs.Thickness = mobileVM.Specs.Thickness;
                CreatedMobile.Specs.Weight = mobileVM.Specs.Weight;

                await _service.AddAsync(CreatedMobile);                

                
                var NewMediaList = new List<Media>();

                if (mobileVM.Media != null && mobileVM.Media.Count > 0)
                {
                    foreach(var media in mobileVM.Media)
                    {
                        string UploadsFolder;

                        if (media.ContentType.ToLower().Contains("image"))
                        {
                            UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                        }
                        else
                        {
                            UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Videos");
                        }

                        UniqueFileNameMed = Guid.NewGuid().ToString() + "_" + media.FileName;
                        FilePath = Path.Combine(UploadsFolder, UniqueFileNameMed);
                        media.CopyTo(new FileStream(FilePath, FileMode.Create));
                        Media SingleMedia;

                        if (media.ContentType.ToLower().Contains("image"))
                        {
                            SingleMedia = new Media() { Type = MediaType.Photo, URL = $"/Images/{UniqueFileNameMed}", MobileId = CreatedMobile.Id };
                        }
                        else
                        {
                            SingleMedia = new Media() { Type = MediaType.Video, URL = $"/Videos/{UniqueFileNameMed}", MobileId = CreatedMobile.Id };
                        }
                        await _mediaService.AddAsync(SingleMedia);
                        NewMediaList.Add(SingleMedia);
                    }
                }

                CreatedMobile.Media = NewMediaList;
                await _service.UpdateAsync(CreatedMobile.Id, CreatedMobile);


                return RedirectToAction(nameof(Index));
            }

            ViewBag.Manufactures = await _serviceMan.GetAllAsync();
            return View(mobileVM);
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
