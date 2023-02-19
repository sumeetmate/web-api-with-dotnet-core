using Microsoft.EntityFrameworkCore;
using USWalks.Models.Domain;

namespace USWalks.Data
{
    public class USWalksDbContext : DbContext
    {
        public USWalksDbContext(DbContextOptions<USWalksDbContext> options) : base(options)
        {

        }

        public DbSet<Region> Region { get; set; }

        public DbSet<Walk> Walk { get; set; }

        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
