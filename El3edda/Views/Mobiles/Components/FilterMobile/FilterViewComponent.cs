using Microsoft.AspNetCore.Mvc;
using El3edda.Data;
using El3edda.Models;
using El3edda.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using El3edda.Data.Enums;

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

            var manufacrer_checks = manufacturers_list
                .Select(m => new Tuple<bool, Manufacturer>(
                    searchParamter?.manufacturerids?.Contains(m.Id) ?? false,
                    m));

            #region enums
            var screens = Enum.GetNames(typeof(ScreenEnum));
            var screens_checks = screens
                .Select(s => new Tuple<bool, ScreenEnum>(
                    searchParamter?.Screens?.Contains(Enum.Parse<ScreenEnum>(s)) ?? false,
                    Enum.Parse<ScreenEnum>(s)
                    ));
            


            var os_es = Enum.GetNames(typeof(OSEnum));
            var os_checks = os_es
                .Select(s => new Tuple<bool, OSEnum>(
                    searchParamter?.OS?.Contains(Enum.Parse<OSEnum>(s)) ?? false,
                    Enum.Parse<OSEnum>(s)
                    ));


            var colors = Enum.GetNames(typeof(Colors));
            var colors_checks = colors
                .Select(s => new Tuple<bool, Colors>(
                    searchParamter?.Colors?.Contains(Enum.Parse<Colors>(s)) ?? false,
                    Enum.Parse<Colors>(s)
                    ));

            // TODO add cpu 

            #endregion
            ViewBag.colors = colors_checks;
            ViewBag.os_es = os_checks;
            ViewBag.screens = screens_checks;
			ViewBag.Manufacturers = manufacrer_checks;


            return View(searchParamter);
        }

    }
}
