using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHumanRightsSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUsHumanRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    TenkilMuseumUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InstagramUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsHumanRights", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsHumanRights");
        }
    }
}
