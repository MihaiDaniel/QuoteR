using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Framework.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreDataToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Quotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuoteIndex",
                table: "Quotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Collections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Collections",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChapterIndex",
                table: "Chapters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Chapters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "QuoteIndex",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ChapterIndex",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");
        }
    }
}
