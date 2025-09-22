﻿using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending=true, int pageNumber=1, int pageSize=1000)
        {
            //FILTERING
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
        if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));

                }
            }
            //SORTING
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks= isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x=> x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //PAGINATION
            var skipResults = (pageNumber - 1) * pageSize;

          
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
           var existingWalk = dbContext.Walks.Find(id);
           if(existingWalk == null)
            {
                return null;
            }
             existingWalk.Name = walk.Name;
             existingWalk.LengthInKm = walk.LengthInKm;
             existingWalk.WalkImageUrl = walk.WalkImageUrl;
             existingWalk.Description = walk.Description;
             existingWalk.DifficultyId = walk.DifficultyId;
             existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}
