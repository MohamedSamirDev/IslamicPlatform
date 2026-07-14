using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IslamicPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTextArabicNormalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextArabicNormalized",
                table: "Ayahs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextArabicNormalized",
                table: "Ayahs");
        }
    }
}
