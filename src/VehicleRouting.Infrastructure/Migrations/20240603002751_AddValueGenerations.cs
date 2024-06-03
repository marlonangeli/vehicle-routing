using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRouting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddValueGenerations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Drivers_DriverId",
                table: "WorkSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSchedules",
                table: "WorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_Id",
                table: "WorkSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Id",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_Id",
                table: "Places");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_Id",
                table: "Drivers");

            migrationBuilder.RenameTable(
                name: "WorkSchedules",
                newName: "WorkSchedule");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "Driver");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_DriverId",
                table: "WorkSchedule",
                newName: "IX_WorkSchedule_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSchedule",
                table: "WorkSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Driver",
                table: "Driver",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedule_Driver_DriverId",
                table: "WorkSchedule",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedule_Driver_DriverId",
                table: "WorkSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkSchedule",
                table: "WorkSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Driver",
                table: "Driver");

            migrationBuilder.RenameTable(
                name: "WorkSchedule",
                newName: "WorkSchedules");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.RenameTable(
                name: "Driver",
                newName: "Drivers");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedule_DriverId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkSchedules",
                table: "WorkSchedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_Id",
                table: "WorkSchedules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Id",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Places_Id",
                table: "Places",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Id",
                table: "Drivers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Drivers_DriverId",
                table: "WorkSchedules",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
