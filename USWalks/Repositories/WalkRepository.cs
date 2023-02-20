using Microsoft.EntityFrameworkCore;
using USWalks.Data;
using USWalks.Models.Domain;

namespace USWalks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly USWalksDbContext _dbContext;

        public WalkRepository(USWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.ID = Guid.NewGuid();
            await _dbContext.Walk.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk_db = await _dbContext.Walk.FindAsync(id);
            if (walk_db == null) { return null; }

            _dbContext.Walk.Remove(walk_db);
            await _dbContext.SaveChangesAsync();

            return walk_db;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _dbContext.Walk
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await _dbContext.Walk
                .Include(w => w.Region)
                .Include(w => w.WalkDifficulty)
                .FirstOrDefaultAsync(w => w.ID == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
           var walk_db =  await _dbContext.Walk.FindAsync(id);

            if(walk_db == null) { return null; }

            walk_db.Length = walk.Length;
            walk_db.WalkDifficultyId = walk.WalkDifficultyId;
            walk_db.RegionId = walk.RegionId;
            walk_db.Name = walk.Name;
            await _dbContext.SaveChangesAsync();

            return walk_db;
        }
    }
}
