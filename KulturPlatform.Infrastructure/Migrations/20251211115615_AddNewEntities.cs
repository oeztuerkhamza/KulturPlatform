using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonatePages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeroTitleTurkish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HeroTitleGerman = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HeroSubtitleTurkish = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    HeroSubtitleGerman = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    HeroImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AccountHolder = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentTurkish = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ContentGerman = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonatePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuelenMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTurkish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleGerman = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentTurkish = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ContentGerman = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuelenMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Turkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    German = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    English = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satzungs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTurkish = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleGerman = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ContentTurkish = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ContentGerman = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satzungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeaEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTurkish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleGerman = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Date = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationResources_IsActive",
                table: "LocalizationResources",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationResources_Key",
                table: "LocalizationResources",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationResources_Section",
                table: "LocalizationResources",
                column: "Section");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonatePages");

            migrationBuilder.DropTable(
                name: "GuelenMovements");

            migrationBuilder.DropTable(
                name: "LocalizationResources");

            migrationBuilder.DropTable(
                name: "Satzungs");

            migrationBuilder.DropTable(
                name: "TeaEvents");
        }
    }
}
