using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalWinRegionCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalWinRegionCode",
                table: "AppRegistrations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalWinRegionCode",
                table: "AppRegistrations");
        }
    }
}
