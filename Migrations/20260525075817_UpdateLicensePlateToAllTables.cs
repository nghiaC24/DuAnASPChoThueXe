using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnASPChoThueXe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLicensePlateToAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "CartItems");
        }
    }
}
