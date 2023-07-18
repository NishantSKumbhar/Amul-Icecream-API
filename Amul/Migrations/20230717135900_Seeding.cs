using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Amul.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("45364c4e-4b50-4c58-9c12-ca95e3af1c18"), "Comes in a cup structure.", "Cups" },
                    { new Guid("482eba9f-42b0-452a-9b1d-5b16c9b97228"), "Comes in a stick with ice-creme structure.", "Kulfi" },
                    { new Guid("771ee197-0f61-4fbf-994b-9772393fbbdc"), "Comes in a cone structure.", "Tri Cone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("45364c4e-4b50-4c58-9c12-ca95e3af1c18"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("482eba9f-42b0-452a-9b1d-5b16c9b97228"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("771ee197-0f61-4fbf-994b-9772393fbbdc"));
        }
    }
}
