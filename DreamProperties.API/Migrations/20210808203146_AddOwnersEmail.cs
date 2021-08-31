using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamProperties.API.Migrations
{
    public partial class AddOwnersEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnersEmail",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnersEmail",
                table: "Properties");
        }
    }
}
