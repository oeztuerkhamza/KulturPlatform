using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGalleryImageDataSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ActivityGalleryImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "ImageData_Base64",
                table: "ActivityGalleryImages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageData_FileName",
                table: "ActivityGalleryImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageData_FileSize",
                table: "ActivityGalleryImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageData_MimeType",
                table: "ActivityGalleryImages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData_Base64",
                table: "ActivityGalleryImages");

            migrationBuilder.DropColumn(
                name: "ImageData_FileName",
                table: "ActivityGalleryImages");

            migrationBuilder.DropColumn(
                name: "ImageData_FileSize",
                table: "ActivityGalleryImages");

            migrationBuilder.DropColumn(
                name: "ImageData_MimeType",
                table: "ActivityGalleryImages");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ActivityGalleryImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
