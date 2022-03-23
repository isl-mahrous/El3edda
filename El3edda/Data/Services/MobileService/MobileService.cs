using El3edda.Data.Base;
using El3edda.Models;

namespace El3edda.Data.Services.MobileService
{
    public class MobileService : EntityBaseRepository<Mobile>, IMobileService
    {
        public MobileService(AppDbContext context) : base(context) { }
    }
}
