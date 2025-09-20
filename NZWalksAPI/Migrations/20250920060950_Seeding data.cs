using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2cb16262-dccf-40e6-971b-5a8ddc25c644"), "Hard" },
                    { new Guid("72c523de-27ac-4a30-b9f6-b89370250c3b"), "Easy" },
                    { new Guid("c4821f51-bcb3-45dd-beaf-92fe0457460e"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1eed54c3-5ba5-44ae-b8b4-d5b2695537d3"), "WLG", "Wellington", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/Wellington_City_from_Mt_Victoria_looking_towards_the_Basin_Reserve_and_City_Centre_%282%29.jpg/1920px-Wellington_City_from_Mt_Victoria_looking_towards_the_Basin_Reserve_and_City_Centre_%282%29.jpg" },
                    { new Guid("7e6b548f-55cf-4936-90d8-dec24ef01410"), "AKL", "Aukland", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Auckland_Marina_%28273797355%29.jpeg/1920px-Auckland_Marina_%28273797355%29.jpeg" },
                    { new Guid("d3b5c6e7-8f9a-4b1c-9d2e-3f4a5b6c7d8e"), "HML", "Hamilton", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Hamilton_City_Centre_from_Hamilton_Lake_looking_north_%282%29.jpg/1920px-Hamilton_City_Centre_from_Hamilton_Lake_looking_north_%282%29.jpg" },
                    { new Guid("f4d2b3f1-2dcb-4c8a-9c8a-2b5f6e3e8e3a"), "CHC", "Christchurch", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Christchurch_City_Centre_from_Bridge_of_Remembrance_looking_south_%282%29.jpg/1920px-Christchurch_City_Centre_from_Bridge_of_Remembrance_looking_south_%282%29.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2cb16262-dccf-40e6-971b-5a8ddc25c644"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("72c523de-27ac-4a30-b9f6-b89370250c3b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c4821f51-bcb3-45dd-beaf-92fe0457460e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1eed54c3-5ba5-44ae-b8b4-d5b2695537d3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7e6b548f-55cf-4936-90d8-dec24ef01410"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d3b5c6e7-8f9a-4b1c-9d2e-3f4a5b6c7d8e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f4d2b3f1-2dcb-4c8a-9c8a-2b5f6e3e8e3a"));
        }
    }
}
