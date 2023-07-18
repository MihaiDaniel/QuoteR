using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quoter.Framework.Migrations
{
    /// <inheritdoc />
    public partial class AddImportIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImportChapterId",
                table: "Chapters",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImportBookId",
                table: "Books",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportChapterId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "ImportBookId",
                table: "Books");
        }
    }
}
