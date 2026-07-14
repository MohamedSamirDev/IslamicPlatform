using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IslamicPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTextArabicSearchTOSurah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameArabicNormalized",
                table: "Surahs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameArabicNormalized",
                table: "Surahs");
        }
    }
}
