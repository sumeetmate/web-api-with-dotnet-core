using Microsoft.EntityFrameworkCore;
using USWalks.Data;
using USWalks.Models.Domain;

namespace USWalks.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly USWalksDbContext _context;

        public RegionsRepository(USWalksDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.ID = Guid.NewGuid();
            await _context.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _context.Region.FirstOrDefaultAsync(r => r.ID == id);

            if (region == null) { return null; }

            _context.Region.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public IEnumerable<Region> GetAll()
        {
            return _context.Region.ToList();
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Region.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            return await _context.Region.FirstOrDefaultAsync(r => r.ID == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var  db_region  =  await _context.Region.FirstOrDefaultAsync(r => r.ID == id);

            if (db_region == null) return null;

            db_region.Code = region.Code;
            db_region.Name = region.Name;
            db_region.Populations = region.Populations;
            db_region.Lat = region.Lat;
            db_region.Long = region.Long;
            db_region.Area = region.Area;

            await _context.SaveChangesAsync();
            return db_region;
        }
    }
}
