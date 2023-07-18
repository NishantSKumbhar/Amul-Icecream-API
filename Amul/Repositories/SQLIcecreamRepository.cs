using Amul.Data;
using Amul.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Amul.Repositories
{
    public class SQLIcecreamRepository : IIcecreamRepository
    {
        private readonly AmulDbContext amulDbContext;

        public SQLIcecreamRepository(AmulDbContext amulDbContext)
        {
            this.amulDbContext = amulDbContext;
        }

        public async Task<List<Icecream>> GetAllIcecreamsAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            //var Icecreams = await amulDbContext.Icecreams.Include("Category").ToListAsync();
            var Icecreams = amulDbContext.Icecreams.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                // check if user wants to filter by the Names column in database.
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Icecreams = Icecreams.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Icecreams = isAscending ? Icecreams.OrderBy(x => x.Name) : Icecreams.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    Icecreams = isAscending ? Icecreams.OrderBy(x => x.Price) : Icecreams.OrderByDescending(x => x.Price);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await Icecreams.Skip(skipResults).Take(pageSize).Include("Category").ToListAsync();
        }

            public async Task<Icecream?> GetIcecreamAsync(Guid id)
            {
                var Icecream = await amulDbContext.Icecreams.Include("Category").FirstOrDefaultAsync(x => x.Id == id);

                return Icecream;
            }

            public async Task<Icecream?> PostIcecreamAsync(Icecream icecream)
            {
                await amulDbContext.Icecreams.AddAsync(icecream);
                await amulDbContext.SaveChangesAsync();
                return icecream;
            }

            public async Task<Icecream?> UpdateIcecreamAsync(Guid id, Icecream icecream)
            {
                var Icecream = await amulDbContext.Icecreams.FirstOrDefaultAsync(x => x.Id == id);
                if (Icecream == null)
                {
                    return null;
                }
                Icecream.Name = icecream.Name;
                Icecream.Description = icecream.Description;
                Icecream.Price = icecream.Price;
                Icecream.CategoryId = icecream.CategoryId;
                Icecream.ImageUrl = icecream.ImageUrl;
                Icecream.Quantity = icecream.Quantity;
                Icecream.IsAvailable = icecream.IsAvailable;

                await amulDbContext.SaveChangesAsync();
                return Icecream;
            }

            public async Task<Icecream?> DeleteIcecreamAsync(Guid id)
            {
                var Icecream = await amulDbContext.Icecreams.FirstOrDefaultAsync(x => x.Id == id);
                amulDbContext.Icecreams.Remove(Icecream);
                await amulDbContext.SaveChangesAsync();
                return Icecream;
            }
    }
    
}
