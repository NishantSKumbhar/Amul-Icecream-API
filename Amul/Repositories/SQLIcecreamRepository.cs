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

        public async Task<List<Icecream>> GetAllIcecreamsAsync()
        {
            var Icecreams = await amulDbContext.Icecreams.Include("Category").ToListAsync();
          
            return Icecreams;
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
            if(Icecream == null)
            {
                return null;
            }
            Icecream.Name = icecream.Name;
            Icecream.Description = icecream.Description;
            Icecream.Price = icecream.Price;
            Icecream.CategoryId = icecream.CategoryId;
            Icecream.ImageUrl = icecream.ImageUrl;
            Icecream.Quantity= icecream.Quantity;
            Icecream.IsAvailable= icecream.IsAvailable;

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
