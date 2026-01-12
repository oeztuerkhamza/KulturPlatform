using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    QuoteDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    QuoteAuthor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WhoWeAreTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    WhoWeAreDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    GoalsTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    GoalsDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VisionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VisionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MissionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MissionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DetailedContentTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    DetailedContentDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    DateIso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_HouseNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "User"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location_HouseNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Twitter = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OfficeHours = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DetailsTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DetailsDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ScheduleTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ScheduleDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Instructor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseLocation_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CourseLocation_HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseLocation_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CourseLocation_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CourseLocation_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CourseLocation_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CourseCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuelenMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Imprints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrganizationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imprints", x => x.Id);
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
                name: "PageContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SectionKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContentTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ContentDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Satzungs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleTurkish = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TitleGerman = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NameAndSeatTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAndSeatTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameAndSeatTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameAndSeatGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameAndSeatGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameAndSeatGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameDescTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameDescTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameDescTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameDescGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameDescGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NameDescGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SeatTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SeatGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatDescTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SeatDescTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatDescTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatDescGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SeatDescGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SeatDescGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FiscalYearTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FiscalYearGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearDescTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FiscalYearDescTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearDescTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearDescGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FiscalYearDescGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FiscalYearDescGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PurposeOfAssociationTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PurposeOfAssociationTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PurposeOfAssociationTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PurposeOfAssociationGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PurposeOfAssociationGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PurposeOfAssociationGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    GemeinnuetzigkeitTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GemeinnuetzigkeitTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    GemeinnuetzigkeitTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    GemeinnuetzigkeitGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GemeinnuetzigkeitGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    GemeinnuetzigkeitGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PoliticalNeutralityTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PoliticalNeutralityTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PoliticalNeutralityTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PoliticalNeutralityGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PoliticalNeutralityGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PoliticalNeutralityGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IntroTr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntroDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeritageTextTr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeritageTextDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipationTextTr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipationTextDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValueItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubtitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubtitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerSubmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerSubmissions", x => x.Id);
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
                    AboutUsId_FocusAreas = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AboutUsId_TeamMembers = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutUsItems_AboutUs_AboutUsId_ActivityAreas",
                        column: x => x.AboutUsId_ActivityAreas,
                        principalTable: "AboutUs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AboutUsItems_AboutUs_AboutUsId_CoreValues",
                        column: x => x.AboutUsId_CoreValues,
                        principalTable: "AboutUs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AboutUsItems_AboutUs_AboutUsId_FocusAreas",
                        column: x => x.AboutUsId_FocusAreas,
                        principalTable: "AboutUs",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActivityGalleryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityGalleryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityGalleryImages_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SatzungMemberships",
                columns: table => new
                {
                    SatzungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Type_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionTurkish_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTurkish_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionTurkish_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionGerman_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionGerman_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionGerman_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SatzungPurposes",
                columns: table => new
                {
                    SatzungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letter = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Content_Heading = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content_BodyTurkish = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Content_BodyGerman = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ActivityGalleryImages_ActivityId",
                table: "ActivityGalleryImages",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Satzungs_Key",
                table: "Satzungs",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValueItemSections_ValueItemId",
                table: "ValueItemSections",
                column: "ValueItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsItems");

            migrationBuilder.DropTable(
                name: "AboutUsTeamMembers");

            migrationBuilder.DropTable(
                name: "ActivityGalleryImages");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "DonatePages");

            migrationBuilder.DropTable(
                name: "GuelenMovements");

            migrationBuilder.DropTable(
                name: "Imprints");

            migrationBuilder.DropTable(
                name: "LocalizationResources");

            migrationBuilder.DropTable(
                name: "PageContents");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "SatzungMemberships");

            migrationBuilder.DropTable(
                name: "SatzungPurposes");

            migrationBuilder.DropTable(
                name: "SectionItems");

            migrationBuilder.DropTable(
                name: "TeaEvents");

            migrationBuilder.DropTable(
                name: "VolunteerSubmissions");

            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Satzungs");

            migrationBuilder.DropTable(
                name: "ValueItemSections");

            migrationBuilder.DropTable(
                name: "ValueItems");
        }
    }
}
