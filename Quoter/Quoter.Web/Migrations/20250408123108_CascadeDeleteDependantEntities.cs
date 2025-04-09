using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Web.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteDependantEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVersionDownloads_AppRegistrations_AppRegistrationId",
                table: "AppVersionDownloads");

            migrationBuilder.DropForeignKey(
                name: "FK_AppVersionDownloads_AppVersions_AppVersionId",
                table: "AppVersionDownloads");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVersionDownloads_AppRegistrations_AppRegistrationId",
                table: "AppVersionDownloads",
                column: "AppRegistrationId",
                principalTable: "AppRegistrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppVersionDownloads_AppVersions_AppVersionId",
                table: "AppVersionDownloads",
                column: "AppVersionId",
                principalTable: "AppVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVersionDownloads_AppRegistrations_AppRegistrationId",
                table: "AppVersionDownloads");

            migrationBuilder.DropForeignKey(
                name: "FK_AppVersionDownloads_AppVersions_AppVersionId",
                table: "AppVersionDownloads");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVersionDownloads_AppRegistrations_AppRegistrationId",
                table: "AppVersionDownloads",
                column: "AppRegistrationId",
                principalTable: "AppRegistrations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVersionDownloads_AppVersions_AppVersionId",
                table: "AppVersionDownloads",
                column: "AppVersionId",
                principalTable: "AppVersions",
                principalColumn: "Id");
        }
    }
}
