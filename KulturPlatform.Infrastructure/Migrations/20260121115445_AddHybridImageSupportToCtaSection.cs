using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHybridImageSupportToCtaSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImageUrl",
                table: "CtaSections",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage_Base64",
                table: "CtaSections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage_FileName",
                table: "CtaSections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundImage_FileSize",
                table: "CtaSections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage_MimeType",
                table: "CtaSections",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImageUrl",
                table: "CtaSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_Base64",
                table: "CtaSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_FileName",
                table: "CtaSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_FileSize",
                table: "CtaSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_MimeType",
                table: "CtaSections");
        }
    }
}
