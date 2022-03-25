using El3edda.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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

        public virtual DbSet<Mobile> Mobiles { get; set; } 
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }


        //////////// ORDERS //////////////

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
