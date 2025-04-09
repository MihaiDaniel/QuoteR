using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Web.Migrations
{
    /// <inheritdoc />
    public partial class SwitchGuidsToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "AppVersions",
                newName: "IsReleased");

            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "AppRegistrations",
                newName: "RegistrationId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppVersions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "AppVersions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppVersionId",
                table: "AppVersionDownloads",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppRegistrationId",
                table: "AppVersionDownloads",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AppRegistrations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "InstallId",
                table: "AppRegistrations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppRegistrationId",
                table: "AppErrors",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "AppRegistrationId",
                table: "AppCollectionDownloads",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "AppVersions");

            migrationBuilder.DropColumn(
                name: "InstallId",
                table: "AppRegistrations");

            migrationBuilder.RenameColumn(
                name: "IsReleased",
                table: "AppVersions",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "AppRegistrations",
                newName: "Identifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AppVersions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppVersionId",
                table: "AppVersionDownloads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppRegistrationId",
                table: "AppVersionDownloads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AppRegistrations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppRegistrationId",
                table: "AppErrors",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppRegistrationId",
                table: "AppCollectionDownloads",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
