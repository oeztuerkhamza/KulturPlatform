using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KulturPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnHouseNoByContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location_Address",
                table: "ContactInfos",
                newName: "Location_Street");

            migrationBuilder.RenameColumn(
                name: "Address_HouseNo",
                table: "ContactInfos",
                newName: "Location_HouseNo");

            migrationBuilder.AlterColumn<string>(
                name: "Location_HouseNo",
                table: "ContactInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location_Street",
                table: "ContactInfos",
                newName: "Location_Address");

            migrationBuilder.RenameColumn(
                name: "Location_HouseNo",
                table: "ContactInfos",
                newName: "Address_HouseNo");

            migrationBuilder.AlterColumn<string>(
                name: "Address_HouseNo",
                table: "ContactInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
