using Amul.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Amul.Data
{
    public class AmulDbContext: DbContext
    {
        //public AmulDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        //{

        //}
        //DbContextOptions<AmulDbContext>  else gives error after injection into programs.cs file.
        // because we have now two DbContext
        public AmulDbContext(DbContextOptions<AmulDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        // When we run entity core migrations, Below two properties will create tables in database.
        public DbSet<Icecream> Icecreams { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed the Data For the Categories
            // id, Name , Description
            // Guid.NewGuid() in C# interacive environment.

            var Categories = new List<Categories>
            {
                new Categories()
                {
                    Id = Guid.Parse("771ee197-0f61-4fbf-994b-9772393fbbdc"),
                    Name = "Tri Cone",
                    Description = "Comes in a cone structure."
                },
                new Categories()
                {
                    Id = Guid.Parse("45364c4e-4b50-4c58-9c12-ca95e3af1c18"),
                    Name = "Cups",
                    Description = "Comes in a cup structure."
                },
                new Categories()
                {
                    Id = Guid.Parse("482eba9f-42b0-452a-9b1d-5b16c9b97228"),
                    Name = "Kulfi",
                    Description = "Comes in a stick with ice-creme structure."
                }
            };

            modelBuilder.Entity<Categories>().HasData(Categories);
            // after we have to migrate.


            var Icecreams = new List<Icecream>
            {
                new Icecream()
                {
                    Id = Guid.Parse("0fe7d888-a10a-4c0d-af0c-8ab292451cb1"),
                    Name = "Kesar Pista",
                    Description = "Ingredients ; 3 liters Milk , (I use 2% fat) ; 1/2 cup Sugar ; 1 teaspoon Saffron strands ; 2 teaspoons Cardamom Powder (Elaichi) ",
                    CategoryId = Guid.Parse("771ee197-0f61-4fbf-994b-9772393fbbdc"),
                    Quantity = "120ml",
                    IsAvailable = true,
                    Price = 30,
                    ImageUrl = "https://static.toiimg.com/photo/84786580.cms"
                },
                new Icecream()
                {
                    Id = Guid.Parse("f7847a7a-1b5a-4e7f-8306-41bedff26b30"),
                    Name = "CHOCO VANILLA",
                    Description = "The classic combination of Chocolate and Vanilla, made better with a Wafer Biscuit Cone. That sounds like a dream come true!",
                    CategoryId = Guid.Parse("771ee197-0f61-4fbf-994b-9772393fbbdc"),
                    Quantity = "120ml., 40ml",
                    IsAvailable = true,
                    Price = 35,
                    ImageUrl = "https://www.havmor.com/sites/default/files/styles/502x375/public/gallery/Choco%20Vanilla_0.webp?itok=7JCWx_ug"
                },
                new Icecream()
                {
                    Id = Guid.Parse("94d81027-a2f6-4edf-b313-f1ff1d46b65f"),
                    Name = "Pista Badam",
                    Description = "Ingredients :Half Liter Milk1 Cup Whipping Cream Half Cup SugarBadam, Pista - 100 GramsPinch of Cardamom Powder Pinch of Food ColorMusic",
                    CategoryId = Guid.Parse("771ee197-0f61-4fbf-994b-9772393fbbdc"),
                    Quantity = "120ml",
                    IsAvailable = true,
                    Price = 30,
                    ImageUrl = "https://www.factoryrates.in/wp-content/uploads/2021/09/IC-Tricone-Pista-Badam-120ml-20x6-1.jpg"
                },
            };
            modelBuilder.Entity<Icecream>().HasData(Icecreams);
            // after migrate
        }
    }
}
