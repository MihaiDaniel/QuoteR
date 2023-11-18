using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Web.Migrations
{
    public partial class AddMoreVersionInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "AppVersions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppVersions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "AppVersions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppVersions");
        }
    }
}
