using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Amul.Migrations
{
    /// <inheritdoc />
    public partial class SeedingIcecream : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Icecreams",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0fe7d888-a10a-4c0d-af0c-8ab292451cb1"), new Guid("771ee197-0f61-4fbf-994b-9772393fbbdc"), "Ingredients ; 3 liters Milk , (I use 2% fat) ; 1/2 cup Sugar ; 1 teaspoon Saffron strands ; 2 teaspoons Cardamom Powder (Elaichi) ", "https://static.toiimg.com/photo/84786580.cms", true, "Kesar Pista", 30, "120ml" },
                    { new Guid("94d81027-a2f6-4edf-b313-f1ff1d46b65f"), new Guid("771ee197-0f61-4fbf-994b-9772393fbbdc"), "Ingredients :Half Liter Milk1 Cup Whipping Cream Half Cup SugarBadam, Pista - 100 GramsPinch of Cardamom Powder Pinch of Food ColorMusic", "https://www.factoryrates.in/wp-content/uploads/2021/09/IC-Tricone-Pista-Badam-120ml-20x6-1.jpg", true, "Pista Badam", 30, "120ml" },
                    { new Guid("f7847a7a-1b5a-4e7f-8306-41bedff26b30"), new Guid("771ee197-0f61-4fbf-994b-9772393fbbdc"), "The classic combination of Chocolate and Vanilla, made better with a Wafer Biscuit Cone. That sounds like a dream come true!", "https://www.havmor.com/sites/default/files/styles/502x375/public/gallery/Choco%20Vanilla_0.webp?itok=7JCWx_ug", true, "CHOCO VANILLA", 35, "120ml., 40ml" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Icecreams",
                keyColumn: "Id",
                keyValue: new Guid("0fe7d888-a10a-4c0d-af0c-8ab292451cb1"));

            migrationBuilder.DeleteData(
                table: "Icecreams",
                keyColumn: "Id",
                keyValue: new Guid("94d81027-a2f6-4edf-b313-f1ff1d46b65f"));

            migrationBuilder.DeleteData(
                table: "Icecreams",
                keyColumn: "Id",
                keyValue: new Guid("f7847a7a-1b5a-4e7f-8306-41bedff26b30"));
        }
    }
}
