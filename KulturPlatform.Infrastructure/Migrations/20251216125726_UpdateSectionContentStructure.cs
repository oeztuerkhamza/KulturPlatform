using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSectionContentStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatTurkish_Body",
                table: "Satzungs",
                newName: "SeatTurkish_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "SeatGerman_Body",
                table: "Satzungs",
                newName: "SeatTurkish_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "SeatDescTurkish_Body",
                table: "Satzungs",
                newName: "SeatGerman_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "SeatDescGerman_Body",
                table: "Satzungs",
                newName: "SeatGerman_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "PurposeOfAssociationTurkish_Body",
                table: "Satzungs",
                newName: "SeatDescTurkish_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "PurposeOfAssociationGerman_Body",
                table: "Satzungs",
                newName: "SeatDescTurkish_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "PoliticalNeutralityTurkish_Body",
                table: "Satzungs",
                newName: "SeatDescGerman_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "PoliticalNeutralityGerman_Body",
                table: "Satzungs",
                newName: "SeatDescGerman_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "NameDescTurkish_Body",
                table: "Satzungs",
                newName: "PurposeOfAssociationTurkish_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "NameDescGerman_Body",
                table: "Satzungs",
                newName: "PurposeOfAssociationTurkish_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "NameAndSeatTurkish_Body",
                table: "Satzungs",
                newName: "PurposeOfAssociationGerman_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "NameAndSeatGerman_Body",
                table: "Satzungs",
                newName: "PurposeOfAssociationGerman_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "GemeinnuetzigkeitTurkish_Body",
                table: "Satzungs",
                newName: "PoliticalNeutralityTurkish_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "GemeinnuetzigkeitGerman_Body",
                table: "Satzungs",
                newName: "PoliticalNeutralityTurkish_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "FiscalYearTurkish_Body",
                table: "Satzungs",
                newName: "PoliticalNeutralityGerman_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "FiscalYearGerman_Body",
                table: "Satzungs",
                newName: "PoliticalNeutralityGerman_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "FiscalYearDescTurkish_Body",
                table: "Satzungs",
                newName: "NameDescTurkish_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "FiscalYearDescGerman_Body",
                table: "Satzungs",
                newName: "NameDescTurkish_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "Content_Body",
                table: "SatzungPurposes",
                newName: "Content_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "Type_Body",
                table: "SatzungMemberships",
                newName: "Type_BodyTurkish");

            migrationBuilder.RenameColumn(
                name: "DescriptionTurkish_Body",
                table: "SatzungMemberships",
                newName: "Type_BodyGerman");

            migrationBuilder.RenameColumn(
                name: "DescriptionGerman_Body",
                table: "SatzungMemberships",
                newName: "DescriptionTurkish_BodyTurkish");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescGerman_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescGerman_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescTurkish_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescTurkish_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearGerman_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearGerman_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearTurkish_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearTurkish_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitGerman_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitGerman_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitTurkish_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitTurkish_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatGerman_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatGerman_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatTurkish_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatTurkish_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescGerman_BodyGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescGerman_BodyTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content_BodyGerman",
                table: "SatzungPurposes",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionGerman_BodyGerman",
                table: "SatzungMemberships",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionGerman_BodyTurkish",
                table: "SatzungMemberships",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionTurkish_BodyGerman",
                table: "SatzungMemberships",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiscalYearDescGerman_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescGerman_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescTurkish_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescTurkish_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearGerman_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearGerman_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearTurkish_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearTurkish_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitGerman_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitGerman_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitTurkish_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitTurkish_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatGerman_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatGerman_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatTurkish_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatTurkish_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescGerman_BodyGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescGerman_BodyTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "Content_BodyGerman",
                table: "SatzungPurposes");

            migrationBuilder.DropColumn(
                name: "DescriptionGerman_BodyGerman",
                table: "SatzungMemberships");

            migrationBuilder.DropColumn(
                name: "DescriptionGerman_BodyTurkish",
                table: "SatzungMemberships");

            migrationBuilder.DropColumn(
                name: "DescriptionTurkish_BodyGerman",
                table: "SatzungMemberships");

            migrationBuilder.RenameColumn(
                name: "SeatTurkish_BodyTurkish",
                table: "Satzungs",
                newName: "SeatTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "SeatTurkish_BodyGerman",
                table: "Satzungs",
                newName: "SeatGerman_Body");

            migrationBuilder.RenameColumn(
                name: "SeatGerman_BodyTurkish",
                table: "Satzungs",
                newName: "SeatDescTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "SeatGerman_BodyGerman",
                table: "Satzungs",
                newName: "SeatDescGerman_Body");

            migrationBuilder.RenameColumn(
                name: "SeatDescTurkish_BodyTurkish",
                table: "Satzungs",
                newName: "PurposeOfAssociationTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "SeatDescTurkish_BodyGerman",
                table: "Satzungs",
                newName: "PurposeOfAssociationGerman_Body");

            migrationBuilder.RenameColumn(
                name: "SeatDescGerman_BodyTurkish",
                table: "Satzungs",
                newName: "PoliticalNeutralityTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "SeatDescGerman_BodyGerman",
                table: "Satzungs",
                newName: "PoliticalNeutralityGerman_Body");

            migrationBuilder.RenameColumn(
                name: "PurposeOfAssociationTurkish_BodyTurkish",
                table: "Satzungs",
                newName: "NameDescTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "PurposeOfAssociationTurkish_BodyGerman",
                table: "Satzungs",
                newName: "NameDescGerman_Body");

            migrationBuilder.RenameColumn(
                name: "PurposeOfAssociationGerman_BodyTurkish",
                table: "Satzungs",
                newName: "NameAndSeatTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "PurposeOfAssociationGerman_BodyGerman",
                table: "Satzungs",
                newName: "NameAndSeatGerman_Body");

            migrationBuilder.RenameColumn(
                name: "PoliticalNeutralityTurkish_BodyTurkish",
                table: "Satzungs",
                newName: "GemeinnuetzigkeitTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "PoliticalNeutralityTurkish_BodyGerman",
                table: "Satzungs",
                newName: "GemeinnuetzigkeitGerman_Body");

            migrationBuilder.RenameColumn(
                name: "PoliticalNeutralityGerman_BodyTurkish",
                table: "Satzungs",
                newName: "FiscalYearTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "PoliticalNeutralityGerman_BodyGerman",
                table: "Satzungs",
                newName: "FiscalYearGerman_Body");

            migrationBuilder.RenameColumn(
                name: "NameDescTurkish_BodyTurkish",
                table: "Satzungs",
                newName: "FiscalYearDescTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "NameDescTurkish_BodyGerman",
                table: "Satzungs",
                newName: "FiscalYearDescGerman_Body");

            migrationBuilder.RenameColumn(
                name: "Content_BodyTurkish",
                table: "SatzungPurposes",
                newName: "Content_Body");

            migrationBuilder.RenameColumn(
                name: "Type_BodyTurkish",
                table: "SatzungMemberships",
                newName: "Type_Body");

            migrationBuilder.RenameColumn(
                name: "Type_BodyGerman",
                table: "SatzungMemberships",
                newName: "DescriptionTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "DescriptionTurkish_BodyTurkish",
                table: "SatzungMemberships",
                newName: "DescriptionGerman_Body");
        }
    }
}
