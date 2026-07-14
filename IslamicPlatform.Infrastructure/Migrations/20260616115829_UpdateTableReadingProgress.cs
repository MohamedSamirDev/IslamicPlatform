using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IslamicPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableReadingProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAyahNumber",
                table: "ReadingProgresses");

            migrationBuilder.DropColumn(
                name: "LastSurahName",
                table: "ReadingProgresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastAyahNumber",
                table: "ReadingProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastSurahName",
                table: "ReadingProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
