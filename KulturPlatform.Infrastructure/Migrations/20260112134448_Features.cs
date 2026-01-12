using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CtaSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PrimaryButtonTr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimaryButtonDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondaryButtonTr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondaryButtonDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonateButtonTr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonateButtonDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CtaSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubtitleTr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SubtitleDe = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BackgroundImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PrimaryButtonTextTr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimaryButtonTextDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondaryButtonTextTr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondaryButtonTextDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstagramPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramPosts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CtaSections");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "HeroSections");

            migrationBuilder.DropTable(
                name: "InstagramPosts");
        }
    }
}
