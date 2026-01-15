using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAboutUsRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AboutUs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AboutUs",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
