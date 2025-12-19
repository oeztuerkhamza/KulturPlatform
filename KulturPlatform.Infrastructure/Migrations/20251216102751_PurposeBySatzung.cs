using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PurposeBySatzung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Satzungs",
                type: "nvarchar(100)",
                maxLength: 100,
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
                name: "NameDescGerman",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDescTurkish",
                table: "Satzungs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SatzungPurposes",
                columns: table => new
                {
                    SatzungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letter = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TextTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TextGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatzungPurposes", x => new { x.SatzungId, x.Id });
                    table.ForeignKey(
                        name: "FK_SatzungPurposes_Satzungs_SatzungId",
                        column: x => x.SatzungId,
                        principalTable: "Satzungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Satzungs_Key",
                table: "Satzungs",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SatzungPurposes");

            migrationBuilder.DropIndex(
                name: "IX_Satzungs_Key",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameAndSeatTurkish",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescGerman",
                table: "Satzungs");

            migrationBuilder.DropColumn(
                name: "NameDescTurkish",
                table: "Satzungs");
        }
    }
}
