using El3edda.Data.Base;
using El3edda.Models;

namespace El3edda.Data.Services.MediaService
{
    public class MediaService : EntityBaseRepository<Media>, IMediaService
    {
        public MediaService(AppDbContext context) : base(context) { }
    }
}
