using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Framework.Migrations
{
    /// <inheritdoc />
    public partial class SwitchGuidsToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VersionId",
                table: "AppVersions",
                newName: "PublicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "AppVersions",
                newName: "VersionId");
        }
    }
}
