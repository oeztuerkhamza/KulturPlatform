using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendGuelenMovementWithSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "ContentTurkish",
                table: "GuelenMovements");

            migrationBuilder.AlterColumn<string>(
                name: "TitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "DialogContentGerman",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DialogContentTurkish",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DialogTitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DialogTitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IntroductionGerman",
                table: "GuelenMovements",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IntroductionTurkish",
                table: "GuelenMovements",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NetworkContentGerman",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NetworkContentTurkish",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NetworkTitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NetworkTitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhilosophyContentGerman",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhilosophyContentTurkish",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhilosophyTitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhilosophyTitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpiritualContentGerman",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpiritualContentTurkish",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpiritualTitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpiritualTitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisionContentGerman",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisionContentTurkish",
                table: "GuelenMovements",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisionTitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisionTitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DialogContentGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "DialogContentTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "DialogTitleGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "DialogTitleTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "IntroductionGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "IntroductionTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "NetworkContentGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "NetworkContentTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "NetworkTitleGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "NetworkTitleTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "PhilosophyContentGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "PhilosophyContentTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "PhilosophyTitleGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "PhilosophyTitleTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "SpiritualContentGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "SpiritualContentTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "SpiritualTitleGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "SpiritualTitleTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "VisionContentGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "VisionContentTurkish",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "VisionTitleGerman",
                table: "GuelenMovements");

            migrationBuilder.DropColumn(
                name: "VisionTitleTurkish",
                table: "GuelenMovements");

            migrationBuilder.AlterColumn<string>(
                name: "TitleTurkish",
                table: "GuelenMovements",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "TitleGerman",
                table: "GuelenMovements",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "ContentGerman",
                table: "GuelenMovements",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentTurkish",
                table: "GuelenMovements",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");
        }
    }
}
