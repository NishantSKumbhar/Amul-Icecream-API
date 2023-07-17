using Amul.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Amul.Data
{
    public class AmulDbContext: DbContext
    {
        public AmulDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        // When we run entity core migrations, Below two properties will create tables in database.
        public DbSet<Icecream> Icecreams { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
