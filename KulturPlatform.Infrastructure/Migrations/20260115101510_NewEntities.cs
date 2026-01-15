using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_ActivityAreas",
                table: "AboutUsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_CoreValues",
                table: "AboutUsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_FocusAreas",
                table: "AboutUsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AboutUsTeamMembers_AboutUs_AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers");

            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AboutUsTeamMembers",
                table: "AboutUsTeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_AboutUsTeamMembers_AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AboutUsItems",
                table: "AboutUsItems");

            migrationBuilder.DropIndex(
                name: "IX_AboutUsItems_AboutUsId_ActivityAreas",
                table: "AboutUsItems");

            migrationBuilder.DropIndex(
                name: "IX_AboutUsItems_AboutUsId_CoreValues",
                table: "AboutUsItems");

            migrationBuilder.DropIndex(
                name: "IX_AboutUsItems_AboutUsId_FocusAreas",
                table: "AboutUsItems");

            migrationBuilder.DropColumn(
                name: "AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers");

            migrationBuilder.DropColumn(
                name: "AboutUsId_ActivityAreas",
                table: "AboutUsItems");

            migrationBuilder.DropColumn(
                name: "AboutUsId_CoreValues",
                table: "AboutUsItems");

            migrationBuilder.DropColumn(
                name: "AboutUsId_FocusAreas",
                table: "AboutUsItems");

            migrationBuilder.DropColumn(
                name: "AboutUsId_TeamMembers",
                table: "AboutUsItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AboutUsItems");

            migrationBuilder.RenameTable(
                name: "AboutUsTeamMembers",
                newName: "TeamMembers");

            migrationBuilder.RenameTable(
                name: "AboutUsItems",
                newName: "FocusAreas");

            migrationBuilder.AlterColumn<string>(
                name: "TitleTr",
                table: "TeamMembers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleDe",
                table: "TeamMembers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TeamMembers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TeamMembers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionTr",
                table: "TeamMembers",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionDe",
                table: "TeamMembers",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeamMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TeamMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeamMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleTr",
                table: "FocusAreas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleDe",
                table: "FocusAreas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionTr",
                table: "FocusAreas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionDe",
                table: "FocusAreas",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FocusAreas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FocusAreas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FocusAreas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FocusAreas",
                table: "FocusAreas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AboutUsGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoalsTr = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    GoalsDe = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsMission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MissionTr = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    MissionDe = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsMission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsQuotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuoteTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    QuoteDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    QuoteAuthor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsQuotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsVision",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisionTr = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    VisionDe = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsVision", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsWhoWeAre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhoWeAreTr = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    WhoWeAreDe = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsWhoWeAre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoreValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleTr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoreValues", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsGoals");

            migrationBuilder.DropTable(
                name: "AboutUsMission");

            migrationBuilder.DropTable(
                name: "AboutUsQuotes");

            migrationBuilder.DropTable(
                name: "AboutUsVision");

            migrationBuilder.DropTable(
                name: "AboutUsWhoWeAre");

            migrationBuilder.DropTable(
                name: "ActivityAreas");

            migrationBuilder.DropTable(
                name: "CoreValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FocusAreas",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FocusAreas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FocusAreas");

            migrationBuilder.RenameTable(
                name: "TeamMembers",
                newName: "AboutUsTeamMembers");

            migrationBuilder.RenameTable(
                name: "FocusAreas",
                newName: "AboutUsItems");

            migrationBuilder.AlterColumn<string>(
                name: "TitleTr",
                table: "AboutUsTeamMembers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TitleDe",
                table: "AboutUsTeamMembers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AboutUsTeamMembers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "AboutUsTeamMembers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionTr",
                table: "AboutUsTeamMembers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionDe",
                table: "AboutUsTeamMembers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleTr",
                table: "AboutUsItems",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "TitleDe",
                table: "AboutUsItems",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionTr",
                table: "AboutUsItems",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "DescriptionDe",
                table: "AboutUsItems",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<Guid>(
                name: "AboutUsId_ActivityAreas",
                table: "AboutUsItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AboutUsId_CoreValues",
                table: "AboutUsItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AboutUsId_FocusAreas",
                table: "AboutUsItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AboutUsId_TeamMembers",
                table: "AboutUsItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AboutUsItems",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboutUsTeamMembers",
                table: "AboutUsTeamMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AboutUsItems",
                table: "AboutUsItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuoteAuthor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GoalsDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    GoalsTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MissionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MissionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    QuoteDe = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    QuoteTr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    VisionDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VisionTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    WhoWeAreDe = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    WhoWeAreTr = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsTeamMembers_AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers",
                column: "AboutUsId_TeamMembers");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_ActivityAreas",
                table: "AboutUsItems",
                column: "AboutUsId_ActivityAreas",
                principalTable: "AboutUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_CoreValues",
                table: "AboutUsItems",
                column: "AboutUsId_CoreValues",
                principalTable: "AboutUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_FocusAreas",
                table: "AboutUsItems",
                column: "AboutUsId_FocusAreas",
                principalTable: "AboutUs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsTeamMembers_AboutUs_AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers",
                column: "AboutUsId_TeamMembers",
                principalTable: "AboutUs",
                principalColumn: "Id");
        }
    }
}
