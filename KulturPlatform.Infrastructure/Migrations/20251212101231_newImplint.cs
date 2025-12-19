using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newImplint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imprints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrganizationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    President = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VicePresident = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LegalStructureTurkish = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LegalStructureGerman = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PurposeTurkish = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PurposeGerman = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TaxExemptionTurkish = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TaxExemptionGerman = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentResponsibilityTurkish = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContentResponsibilityGerman = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LinksResponsibilityTurkish = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LinksResponsibilityGerman = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CopyrightTurkish = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CopyrightGerman = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imprints", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imprints");
        }
    }
}
