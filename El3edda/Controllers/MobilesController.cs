#nullable disable
using System;
using System.IO;
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
using Microsoft.AspNetCore.Identity;

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
        private readonly UserManager<ApplicationUser> _userManager;


        public MobilesController(IMobileService service, IManufacturerService serviceMan,
                                 IWebHostEnvironment hostingEnvironment, IMediaService serviceMed, IReviewService serviceRev, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _serviceMan = serviceMan;
            _hostEnvironment = hostingEnvironment;
            _serviceMed = serviceMed;
            _serviceRev = serviceRev;
            _context = context;
            _userManager = userManager;

        }

        // GET: Mobiles
        public async Task<IActionResult> Index(specSearchParamter searchParam)
        {
            // prepare paramters for filter in ui
            ViewBag.search = searchParam;
            
            // build predicate
            PropSearch searchCriteria = new PropSearch(searchParam);            
            
            var data = await _context.Mobiles.Include(m => m.Manufacturer).Where(searchCriteria.searchPredicate).ToListAsync();
            
            return View(data);
        }


        public async Task<IActionResult> Details(int id)
        {

            var mobile = await _service.GetByIdAsync(id, m => m.Media, m => m.Manufacturer, m => m.Reviews);
            

            var CurrentUser = await _userManager.GetUserAsync(HttpContext.User);

            if(CurrentUser != null)
            {
                ViewBag.CurrUser = CurrentUser;
            }

            ViewBag.Reviews = await _serviceRev.GetAllAsync(R => R.User);


            if (mobile == null)
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
                    UniqueFileName = Guid.NewGuid().ToString() + "_" + mobileVM.MainPhotoURL.FileName;
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
                CreatedMobile.Specs.RAM = mobileVM.Specs.RAM;
                CreatedMobile.Specs.Storage = mobileVM.Specs.Storage;

                await _service.AddAsync(CreatedMobile);


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
            var mobile = await _service.GetByIdAsync(id, m=>m.Media);
            //var mobile = await _context.Mobiles.Include(m => m.Media).FirstOrDefaultAsync(m => m.Id == id);

            if (mobile == null)
            {
                return NotFound();
            }

            var MVM = new MobileViewModel()
            {
                Id = mobile.Id,
                Name = mobile.Name,
                Description = mobile.Description,
                Price = mobile.Price,
                WarrantyPeriod = mobile.WarrantyPeriod,
                ManufacturerId = mobile.ManufacturerId,
                Manufacturer = mobile.Manufacturer,
                UnitsSold = mobile.UnitsSold,
                UnitsInStock = mobile.UnitsInStock,
                ReleaseDate = mobile.ReleaseDate,
                Specs = new Specs()
                {
                    CameraMegaPixels = mobile.Specs.CameraMegaPixels,
                    BatteryCapacity = mobile.Specs.BatteryCapacity,
                    Color = mobile.Specs.Color,
                    OS = mobile.Specs.OS,
                    CPU = mobile.Specs.CPU,
                    Height = mobile.Specs.Height,
                    Weight = mobile.Specs.Weight,
                    Thickness = mobile.Specs.Thickness,
                    Width = mobile.Specs.Width,
                    Screen = mobile.Specs.Screen,
                    RAM = mobile.Specs.RAM,
                    Storage = mobile.Specs.Storage,
                }
            };

            ViewBag.Manufactures = await _serviceMan.GetAllAsync();
            ViewBag.Media = mobile.Media.ToList();
            ViewBag.MainPhotoURL = mobile.MainPhotoURL;

            return View(MVM);
        }

        // POST: Mobiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MobileViewModel mobileVM)
        {
            var mobile = await _service.GetByIdAsync(id, m => m.Media);
            if (id != mobileVM.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) 
            {
                ViewBag.Manufactures = await _serviceMan.GetAllAsync();
                ViewBag.Media = mobile.Media.ToList();
                ViewBag.MainPhotoURL = mobile.MainPhotoURL;
                return View(mobileVM); 
            }

            var editedmobile = await _service.GetByIdAsync(id);
            string UniqueFileName = "";
            string UniqueFileNameMed = "";
            string FilePath = "";

            //If uploaded new main photo change the existing, else don't change

            if (mobileVM.MainPhotoURL != null)
            {
                string UploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + mobileVM.MainPhotoURL.FileName;
                FilePath = Path.Combine(UploadsFolder, UniqueFileName);
                mobileVM.MainPhotoURL.CopyTo(new FileStream(FilePath, FileMode.Create));
                editedmobile.MainPhotoURL = $"/Images/{UniqueFileName}";
            }

            //If uploaded new media files changes the existing, else don't change

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
                editedmobile.Media = NewMediaList;
            }


            editedmobile.Name = mobileVM.Name;
            editedmobile.Price = mobileVM.Price;
            editedmobile.Description = mobileVM.Description;
            editedmobile.UnitsSold = mobileVM.UnitsSold;
            editedmobile.UnitsInStock = mobileVM.UnitsInStock;
            editedmobile.ManufacturerId = mobileVM.ManufacturerId;
            editedmobile.ReleaseDate = mobileVM.ReleaseDate;
            editedmobile.WarrantyPeriod = mobileVM.WarrantyPeriod;

            editedmobile.Specs = new Specs()
            {
                CPU = mobileVM.Specs.CPU,
                OS = mobileVM.Specs.OS,
                Color = mobileVM.Specs.Color,
                BatteryCapacity = mobileVM.Specs.BatteryCapacity,
                Height = mobileVM.Specs.Height,
                Width = mobileVM.Specs.Width,
                Thickness = mobileVM.Specs.Thickness,
                CameraMegaPixels = mobileVM.Specs.CameraMegaPixels,
                Screen = mobileVM.Specs.Screen,
                Weight = mobileVM.Specs.Weight,
                RAM = mobileVM.Specs.RAM,
                Storage = mobileVM.Specs.Storage
            };



            try
            {
                await _service.UpdateAsync(id, editedmobile);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobileExists(editedmobile.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewBag.Manufactures = await _serviceMan.GetAllAsync();
                    ViewBag.Media = mobile.Media.ToList();
                    ViewBag.MainPhotoURL = mobile.MainPhotoURL;
                    return View(mobileVM);
                }
            }
            

        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var mobile = await _service.GetByIdAsync(id, m => m.Media, m => m.Manufacturer, m => m.Reviews);

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
