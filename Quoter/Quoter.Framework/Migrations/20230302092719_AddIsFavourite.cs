using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Framework.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFavourite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Collections",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Chapters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Books",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Books");
        }
    }
}
