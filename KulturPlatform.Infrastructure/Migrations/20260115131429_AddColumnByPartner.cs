using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnByPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionDe",
                table: "Partners",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionTr",
                table: "Partners",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionDe",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "DescriptionTr",
                table: "Partners");
        }
    }
}
