using El3edda.Models;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Many to Many --> OnModelCreating
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder
        //        .Entity<Mobile>()
        //        .OwnsMany(mob => mob.Media, media => media.WithOwner().HasForeignKey("MobileId"));
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<El3edda.Models.Mobile> Mobile { get; set; }

    }
}
