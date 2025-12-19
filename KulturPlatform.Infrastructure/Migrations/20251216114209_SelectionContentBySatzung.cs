using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SelectionContentBySatzung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "ContentTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "TextGerman",
                table: "SatzungPurposes");

            migrationBuilder.RenameColumn(
                name: "NameDescTurkish",
                table: "Satzungs",
                newName: "SeatTurkish_Body");

            migrationBuilder.RenameColumn(
                name: "NameDescGerman",
                table: "Satzungs",
                newName: "SeatGerman_Body");

            migrationBuilder.RenameColumn(
                name: "TextTurkish",
                table: "SatzungPurposes",
                newName: "Content_Body");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearDescTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalYearTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GemeinnuetzigkeitTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PoliticalNeutralityGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PoliticalNeutralityGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PoliticalNeutralityTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PoliticalNeutralityTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfAssociationGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfAssociationGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfAssociationTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurposeOfAssociationTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatDescGerman_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatDescGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatDescTurkish_Body",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatDescTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatGerman_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeatTurkish_Heading",
                table: "Satzungs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content_Heading",
                table: "SatzungPurposes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SatzungMemberships",
                columns: table => new
                {
                    SatzungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type_Body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTurkish_Body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionGerman_Body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatzungMemberships", x => new { x.SatzungId, x.Id });
                    table.ForeignKey(
                        name: "FK_SatzungMemberships_Satzungs_SatzungId",
                        column: x => x.SatzungId,
                        principalTable: "Satzungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SatzungMemberships");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearDescTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "FiscalYearTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "GemeinnuetzigkeitTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PoliticalNeutralityGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PoliticalNeutralityGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PoliticalNeutralityTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PoliticalNeutralityTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PurposeOfAssociationGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PurposeOfAssociationGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PurposeOfAssociationTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "PurposeOfAssociationTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "SeatDescGerman_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "SeatDescGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "SeatDescTurkish_Body",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "SeatDescTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "SeatGerman_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "SeatTurkish_Heading",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "Content_Heading",
                table: "SatzungPurposes");

            migrationBuilder.RenameColumn(
                name: "SeatTurkish_Body",
                table: "Satzungs",
                newName: "NameDescTurkish");

            migrationBuilder.RenameColumn(
                name: "SeatGerman_Body",
                table: "Satzungs",
                newName: "NameDescGerman");

            migrationBuilder.RenameColumn(
                name: "Content_Body",
                table: "SatzungPurposes",
                newName: "TextTurkish");

            migrationBuilder.AddColumn<string>(
                name: "ContentGerman",
                table: "Satzungs",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentTurkish",
                table: "Satzungs",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatGerman",
                table: "Satzungs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAndSeatTurkish",
                table: "Satzungs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextGerman",
                table: "SatzungPurposes",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }
    }
}
