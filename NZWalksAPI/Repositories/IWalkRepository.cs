using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository //Method defintion
    {
        Task <Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();

        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);

    }
}
