using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHybridImageSupportToHeroSectionAndPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo_Base64",
                table: "Partners",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo_FileName",
                table: "Partners",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Logo_FileSize",
                table: "Partners",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo_MimeType",
                table: "Partners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundImageUrl",
                table: "HeroSections",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage_Base64",
                table: "HeroSections",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage_FileName",
                table: "HeroSections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundImage_FileSize",
                table: "HeroSections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage_MimeType",
                table: "HeroSections",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo_Base64",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Logo_FileName",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Logo_FileSize",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Logo_MimeType",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_Base64",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_FileName",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_FileSize",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "BackgroundImage_MimeType",
                table: "HeroSections");

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundImageUrl",
                table: "HeroSections",
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
