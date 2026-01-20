using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageDataToActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageData_Base64",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageData_FileName",
                table: "Activities",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageData_FileSize",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageData_MimeType",
                table: "Activities",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData_Base64",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ImageData_FileName",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ImageData_FileSize",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ImageData_MimeType",
                table: "Activities");
        }
    }
}
