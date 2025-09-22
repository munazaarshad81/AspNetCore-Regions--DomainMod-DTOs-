using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository //Method defintion
    {
        Task <Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn=null, string? filterQuery=null, string? sortBy =null , bool isAscending = true, int pageNumber=1, int pageSize=1000); //filterOn and filterQuery are optional parameters
        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);

    }
}
