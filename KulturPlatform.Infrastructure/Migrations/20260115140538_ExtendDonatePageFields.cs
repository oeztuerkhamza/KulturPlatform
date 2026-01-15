using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendDonatePageFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BicSwift",
                table: "DonatePages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feature1TitleGerman",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feature1TitleTurkish",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feature2TitleGerman",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feature2TitleTurkish",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feature3TitleGerman",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feature3TitleTurkish",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PayPalHandle",
                table: "DonatePages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PayPalUrl",
                table: "DonatePages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxInfoGerman",
                table: "DonatePages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxInfoTurkish",
                table: "DonatePages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhereDescriptionGerman",
                table: "DonatePages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhereDescriptionTurkish",
                table: "DonatePages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhereTitleGerman",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhereTitleTurkish",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhyDonateDescriptionGerman",
                table: "DonatePages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhyDonateDescriptionTurkish",
                table: "DonatePages",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhyDonateTitleGerman",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhyDonateTitleTurkish",
                table: "DonatePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BicSwift",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "Feature1TitleGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "Feature1TitleTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "Feature2TitleGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "Feature2TitleTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "Feature3TitleGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "Feature3TitleTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "PayPalHandle",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "PayPalUrl",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "TaxInfoGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "TaxInfoTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhereDescriptionGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhereDescriptionTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhereTitleGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhereTitleTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhyDonateDescriptionGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhyDonateDescriptionTurkish",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhyDonateTitleGerman",
                table: "DonatePages");

            migrationBuilder.DropColumn(
                name: "WhyDonateTitleTurkish",
                table: "DonatePages");
        }
    }
}
