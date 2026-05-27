using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAnASPChoThueXe.Migrations
{
    /// <inheritdoc />
    public partial class SyncCarIdToCartAndBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Motorbikes_MotorbikeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Motorbikes_MotorbikeId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "MotorbikeId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotorbikeId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CarId",
                table: "CartItems",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Motorbikes_MotorbikeId",
                table: "Bookings",
                column: "MotorbikeId",
                principalTable: "Motorbikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Cars_CarId",
                table: "CartItems",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Motorbikes_MotorbikeId",
                table: "CartItems",
                column: "MotorbikeId",
                principalTable: "Motorbikes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Motorbikes_MotorbikeId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Cars_CarId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Motorbikes_MotorbikeId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CarId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "MotorbikeId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotorbikeId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Motorbikes_MotorbikeId",
                table: "Bookings",
                column: "MotorbikeId",
                principalTable: "Motorbikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Motorbikes_MotorbikeId",
                table: "CartItems",
                column: "MotorbikeId",
                principalTable: "Motorbikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
