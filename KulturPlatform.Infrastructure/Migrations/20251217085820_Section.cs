using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Section : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ValueItemSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeadingTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HeadingDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BodyTr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BodyDe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ValueItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueItemSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueItemSections_ValueItems_ValueItemId",
                        column: x => x.ValueItemId,
                        principalTable: "ValueItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionItems",
                columns: table => new
                {
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionItems", x => new { x.SectionId, x.Id });
                    table.ForeignKey(
                        name: "FK_SectionItems_ValueItemSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "ValueItemSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValueItemSections_ValueItemId",
                table: "ValueItemSections",
                column: "ValueItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionItems");

            migrationBuilder.DropTable(
                name: "ValueItemSections");
        }
    }
}
