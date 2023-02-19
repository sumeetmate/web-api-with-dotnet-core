using USWalks.Models.Domain;

namespace USWalks.Repositories
{
    public interface IRegionsRepository
    {
        IEnumerable<Region> GetAll();

        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region?>  GetAsync(Guid id);

        Task<Region>  AddAsync(Region region);

        Task<Region> DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
