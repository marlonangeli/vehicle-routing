using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRouting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameLicenseColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Licence",
                table: "Vehicle",
                newName: "License");

            migrationBuilder.RenameColumn(
                name: "Licence",
                table: "Driver",
                newName: "License");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "License",
                table: "Vehicle",
                newName: "Licence");

            migrationBuilder.RenameColumn(
                name: "License",
                table: "Driver",
                newName: "Licence");
        }
    }
}
