using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newTableForAboutUs : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AboutUsId_TeamMembers",
                table: "AboutUsItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AboutUsItems");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_ActivityAreas",
                table: "AboutUsItems",
                column: "AboutUsId_ActivityAreas",
                principalTable: "AboutUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_CoreValues",
                table: "AboutUsItems",
                column: "AboutUsId_CoreValues",
                principalTable: "AboutUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsItems_AboutUs_AboutUsId_FocusAreas",
                table: "AboutUsItems",
                column: "AboutUsId_FocusAreas",
                principalTable: "AboutUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUsTeamMembers_AboutUs_AboutUsId_TeamMembers",
                table: "AboutUsTeamMembers",
                column: "AboutUsId_TeamMembers",
                principalTable: "AboutUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
