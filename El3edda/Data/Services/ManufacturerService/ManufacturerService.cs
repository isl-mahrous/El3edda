using El3edda.Data.Base;
using El3edda.Models;

namespace El3edda.Data.Services
{
    public class ManufacturerService : EntityBaseRepository<Manufacturer>, IManufacturerService
    {
        public ManufacturerService(AppDbContext context) : base(context) { }
    }
}
