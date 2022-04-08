using Microsoft.AspNetCore.Mvc;
using El3edda.Data;
using El3edda.Models;
using El3edda.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace El3edda.Views.Mobiles.Components.FilterMobile
{
    public class FilterMobileViewComponent : ViewComponent
    {


        public FilterMobileViewComponent(AppDbContext _context, IManufacturerService manufacturerService)
        {
            Context = _context;
            ManufacturerService = manufacturerService;
        }

        public AppDbContext Context { get; }
        public IManufacturerService ManufacturerService { get; }

        public async Task<IViewComponentResult> InvokeAsync(specSearchParamter searchParamter)
        {
            //var items = await GetItemsAsync(maxPriority, isDone);

            var manufacturers_list =
                await ManufacturerService.GetAllAsync(o => o.Mobiles);

            //if (searchParamter != null)
            //{
            //    foreach (var manfcture in manufacturers_list)
            //    {
            //        if (searchParamter.manufacturerids.Contains(int.Parse(manfcture.Value)))
            //        {
            //            manfcture.Selected = true;
            //        }
            //    }
            //}
            ViewBag.Manufacturers = manufacturers_list;


            return View(searchParamter);
        }

    }
}
