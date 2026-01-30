using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHybridImageToAboutUsWhoWeAreFocusAreaTeaEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TeaEvents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Image_Base64",
                table: "TeaEvents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image_FileName",
                table: "TeaEvents",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Image_FileSize",
                table: "TeaEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image_MimeType",
                table: "TeaEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionTr",
                table: "FocusAreas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionDe",
                table: "FocusAreas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "FocusAreas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon_Base64",
                table: "FocusAreas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon_FileName",
                table: "FocusAreas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Icon_FileSize",
                table: "FocusAreas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon_MimeType",
                table: "FocusAreas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerImageUrl",
                table: "AboutUsWhoWeAre",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerImage_Base64",
                table: "AboutUsWhoWeAre",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerImage_FileName",
                table: "AboutUsWhoWeAre",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BannerImage_FileSize",
                table: "AboutUsWhoWeAre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerImage_MimeType",
                table: "AboutUsWhoWeAre",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Base64",
                table: "TeaEvents");

            migrationBuilder.DropColumn(
                name: "Image_FileName",
                table: "TeaEvents");

            migrationBuilder.DropColumn(
                name: "Image_FileSize",
                table: "TeaEvents");

            migrationBuilder.DropColumn(
                name: "Image_MimeType",
                table: "TeaEvents");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "Icon_Base64",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "Icon_FileName",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "Icon_FileSize",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "Icon_MimeType",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "BannerImageUrl",
                table: "AboutUsWhoWeAre");

            migrationBuilder.DropColumn(
                name: "BannerImage_Base64",
                table: "AboutUsWhoWeAre");

            migrationBuilder.DropColumn(
                name: "BannerImage_FileName",
                table: "AboutUsWhoWeAre");

            migrationBuilder.DropColumn(
                name: "BannerImage_FileSize",
                table: "AboutUsWhoWeAre");

            migrationBuilder.DropColumn(
                name: "BannerImage_MimeType",
                table: "AboutUsWhoWeAre");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TeaEvents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionTr",
                table: "FocusAreas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionDe",
                table: "FocusAreas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
