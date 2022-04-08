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
using El3edda.Data.Services.ReviewService;

namespace El3edda.Controllers
{

    public class MobilesController : Controller
    {

        private readonly IMobileService _service;
        private readonly IManufacturerService _serviceMan;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMediaService _serviceMed;
        private readonly IReviewService _serviceRev;
        private readonly AppDbContext _context;

        public MobilesController(IMobileService service, IManufacturerService serviceMan,
                                 IWebHostEnvironment hostingEnvironment, IMediaService serviceMed, IReviewService serviceRev, AppDbContext context)
        {
            _service = service;
            _serviceMan = serviceMan;
            _hostEnvironment = hostingEnvironment;
            _serviceMed = serviceMed;
            _serviceRev = serviceRev;
            _context = context;

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

            var mobile = _service.GetByIdAsync(id, m => m.Media, m => m.Manufacturer, m => m.Reviews).Result;

            //var mobile = _context.Mobiles.Include(m=>m.Media).Include(m => m.Manufacturer).Include(m => m.Reviews).Where(m => m.Id == id).FirstOrDefault();

            if (mobile == null)
            {
                return NotFound();
            }

            //ViewBag.Media = await _context.Media.Where(m => m.MobileId == id).ToListAsync();

            return View(mobile);
        }

        //public async Task<IActionResult> ViewSpecs(int id)
        //{

        //    var mobile = await _service.GetByIdAsync(id);
            
        //    if(mobile == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(mobile);
        //}

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
                        await _serviceMed.AddAsync(SingleMedia);
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

            var MVM = new MobileViewModel();
            MVM.Id = mobile.Id;
            MVM.Name = mobile.Name;
            MVM.Description = mobile.Description;
            MVM.Price = mobile.Price;
            MVM.WarrantyPeriod = mobile.WarrantyPeriod;
            MVM.ManufacturerId = mobile.ManufacturerId;
            MVM.UnitsSold = mobile.UnitsSold;
            MVM.UnitsInStock = mobile.UnitsInStock;
            MVM.ReleaseDate = mobile.ReleaseDate;

            MVM.Specs = new Specs();
            MVM.Specs.CameraMegaPixels = mobile.Specs.CameraMegaPixels;
            MVM.Specs.BatteryCapacity = mobile.Specs.BatteryCapacity;
            MVM.Specs.Color = mobile.Specs.Color;
            MVM.Specs.OS = mobile.Specs.OS;
            MVM.Specs.CPU = mobile.Specs.CPU;
            MVM.Specs.Height = mobile.Specs.Height;
            MVM.Specs.Weight = mobile.Specs.Weight;
            MVM.Specs.Thickness = mobile.Specs.Thickness;
            MVM.Specs.Width = mobile.Specs.Width;
            MVM.Specs.Screen = mobile.Specs.Screen;
            MVM.Media = new List<IFormFile>();

            ViewBag.Manufactures = await _serviceMan.GetAllAsync();

            return View(MVM);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MobileViewModel mobileVM)
        {
            if (id != mobileVM.Id)
            {
                return NotFound();
            }

            string UniqueFileName = "";
            string UniqueFileNameMed = "";
            string FilePath = "";

            if (mobileVM.MainPhotoURL != null)
            {
                string UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + mobileVM.MainPhotoURL.FileName;
                FilePath = Path.Combine(UploadsFolder, UniqueFileName);
                mobileVM.MainPhotoURL.CopyTo(new FileStream(FilePath, FileMode.Create));
            }


            var editedmobile = await _service.GetByIdAsync(id);

            editedmobile.MainPhotoURL = $"/Images/{UniqueFileName}";

            editedmobile.Name = mobileVM.Name;
            editedmobile.Price = mobileVM.Price;
            editedmobile.Description = mobileVM.Description;
            editedmobile.UnitsSold = mobileVM.UnitsSold;
            editedmobile.UnitsInStock = mobileVM.UnitsInStock;
            editedmobile.ManufacturerId = mobileVM.ManufacturerId;
            editedmobile.ReleaseDate = mobileVM.ReleaseDate;
            editedmobile.WarrantyPeriod = mobileVM.WarrantyPeriod;

            editedmobile.Specs = new Specs();
            editedmobile.Specs.CPU = mobileVM.Specs.CPU;
            editedmobile.Specs.OS = mobileVM.Specs.OS;
            editedmobile.Specs.Color = mobileVM.Specs.Color;
            editedmobile.Specs.BatteryCapacity = mobileVM.Specs.BatteryCapacity;
            editedmobile.Specs.Height = mobileVM.Specs.Height;
            editedmobile.Specs.Width = mobileVM.Specs.Width;
            editedmobile.Specs.Thickness = mobileVM.Specs.Thickness;
            editedmobile.Specs.CameraMegaPixels = mobileVM.Specs.CameraMegaPixels;
            editedmobile.Specs.Screen = mobileVM.Specs.Screen;
            editedmobile.Specs.Weight = mobileVM.Specs.Weight;

            var NewMediaList = new List<Media>();

            if (mobileVM.Media != null && mobileVM.Media.Count > 0)
            {
                foreach (var media in mobileVM.Media)
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

                    var DeletedMediaList = _context.Media.Where(m => m.MobileId == id);
                    foreach (var med in DeletedMediaList)
                    {
                        _context.Media.Remove(med);
                    }

                    if (media.ContentType.ToLower().Contains("image"))
                    {
                        SingleMedia = new Media() { Type = MediaType.Photo, URL = $"/Images/{UniqueFileNameMed}", MobileId = editedmobile.Id };
                    }
                    else
                    {
                        SingleMedia = new Media() { Type = MediaType.Video, URL = $"/Videos/{UniqueFileNameMed}", MobileId = editedmobile.Id };
                    }
                    await _serviceMed.AddAsync(SingleMedia);
                    NewMediaList.Add(SingleMedia);
                }
            }

            editedmobile.Media = NewMediaList;

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(id, editedmobile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileExists(editedmobile.Id))
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
            return View(mobileVM);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var mobile = _service.GetByIdAsync(id, m => m.Media, m => m.Manufacturer, m => m.Reviews).Result;

            //var mobile = _context.Mobiles.Include(m => m.Manufacturer).Where(m => m.Id == id).FirstOrDefault();

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
