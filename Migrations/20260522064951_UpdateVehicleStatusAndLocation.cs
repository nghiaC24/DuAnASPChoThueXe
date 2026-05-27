using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnASPChoThueXe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleStatusAndLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Motorbikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLat",
                table: "Motorbikes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLng",
                table: "Motorbikes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Motorbikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Motorbikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLat",
                table: "Cars",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLng",
                table: "Cars",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "CurrentLat",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "CurrentLng",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CurrentLat",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CurrentLng",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Cars");
        }
    }
}
