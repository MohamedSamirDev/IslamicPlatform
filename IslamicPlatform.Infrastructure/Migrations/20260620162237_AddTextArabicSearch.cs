using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IslamicPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTextArabicSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextArabicSearch",
                table: "Hadiths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextArabicSearch",
                table: "Hadiths");
        }
    }
}
