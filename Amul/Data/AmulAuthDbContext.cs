using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Amul.Data
{
    public class AmulAuthDbContext : IdentityDbContext
    {
        public AmulAuthDbContext(DbContextOptions<AmulAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "6458e13c-7b8c-4811-a558-b0eada5628b6";
            var writerRoleId = "166e8a6e-34cb-4aff-b1aa-972a89707e80";

            var Roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }

            };
            builder.Entity<IdentityRole>().HasData(Roles);
            // now Migrate
            // but check now you have two DbContext so it will gennerate error, if you try to do in same way.
            // now use ->  Add-Migration "creating Auth Database" -Context "AmulAuthDbContext"
            // and also -> Update-Database -Context "AmulAuthDbContext"
        }
    }
}
