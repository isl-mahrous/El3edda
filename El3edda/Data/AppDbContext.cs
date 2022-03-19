using Microsoft.EntityFrameworkCore;

namespace El3edda.Data
{

    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

        //Many to Many --> OnModelCreating
    }
}
