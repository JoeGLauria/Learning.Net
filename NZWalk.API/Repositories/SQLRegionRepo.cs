using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public class SQLRegionRepo : IRegionRepo
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepo(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionExists = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionExists == null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionExists);
            await dbContext.SaveChangesAsync();
            return regionExists;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionExists = await dbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);

            if (regionExists == null)
            {
                return null;
            }

            regionExists.Code = region.Code;
            regionExists.Name = region.Name;
            regionExists.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionExists;
        }
    }
}
