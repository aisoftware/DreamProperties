using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamProperties.API.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "City", "ForSale", "ImageUrl", "NumberOfBedrooms", "NumberOfLikes", "Price", "PropertyType", "SquareMeters", "Title" },
                values: new object[,]
                {
                    { 1, "132, West street, New York, United States", "New York", true, "propertyWide.png", 3, 88, 40500, "House", 1800f, "3 Bedroom Independent House" },
                    { 2, "132, Main street, Los Angeles, United States", "Los Angeles", false, "propertyWide.png", 2, 75, 1800, "Flat", 50f, "2 Bedroom Flat" },
                    { 3, "132, West street, New York, United States", "New York", false, "propertyWide.png", 3, 66, 2200, "Flat", 60f, "3 Bedroom Flat" },
                    { 4, "132, West street, New York, United States", "New York", true, "propertyWide.png", 3, 14, 40500, "House", 1800f, "3 Bedroom Independent House" },
                    { 5, "132, West street, New York, United States", "New York", true, "propertyWide.png", 3, 2, 40500, "House", 1000f, "3 Bedroom Independent House" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
