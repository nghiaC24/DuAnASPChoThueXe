using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnASPChoThueXe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingNationalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CitizenId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Bookings");
        }
    }
}
