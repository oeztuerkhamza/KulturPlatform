using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AboutUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quote = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    QuoteAuthor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WhoWeAreTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    WhoWeAreDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    GoalsTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    GoalsDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VisionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VisionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MissionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MissionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TitleDe = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DescriptionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    DescriptionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    AboutUsId_ActivityAreas = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AboutUsId_CoreValues = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AboutUsId_FocusAreas = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutUsItems_AboutUs_AboutUsId_ActivityAreas",
                        column: x => x.AboutUsId_ActivityAreas,
                        principalTable: "AboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AboutUsItems_AboutUs_AboutUsId_CoreValues",
                        column: x => x.AboutUsId_CoreValues,
                        principalTable: "AboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AboutUsItems_AboutUs_AboutUsId_FocusAreas",
                        column: x => x.AboutUsId_FocusAreas,
                        principalTable: "AboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsTeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TitleTr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TitleDe = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    AboutUsId_TeamMembers = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsTeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutUsTeamMembers_AboutUs_AboutUsId_TeamMembers",
                        column: x => x.AboutUsId_TeamMembers,
                        principalTable: "AboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsItems_AboutUsId_ActivityAreas",
                table: "AboutUsItems",
                column: "AboutUsId_ActivityAreas");

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsItems_AboutUsId_CoreValues",
                table: "AboutUsItems",
                column: "AboutUsId_CoreValues");

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsItems_AboutUsId_FocusAreas",
                table: "AboutUsItems",
                column: "AboutUsId_FocusAreas");

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsTeamMembers_AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers",
                column: "AboutUsId_TeamMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsItems");

            migrationBuilder.DropTable(
                name: "AboutUsTeamMembers");

            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BioDe_De = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    BioDe_Tr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    BioTr_De = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    BioTr_Tr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleDe_De = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleDe_Tr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleTr_De = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleTr_Tr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });
        }
    }
}
