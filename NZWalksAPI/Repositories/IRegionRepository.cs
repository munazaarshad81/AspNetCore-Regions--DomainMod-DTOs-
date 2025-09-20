using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepository //will have definitions of methods that will be implemented in SQLRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task <Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}
