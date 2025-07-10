using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalFileName",
                table: "AppVersions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalFileName",
                table: "AppVersions");
        }
    }
}
