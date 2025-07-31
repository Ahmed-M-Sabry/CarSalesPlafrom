using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSales.Infrastructure.Migrations
{
    public partial class AddFuelTypeAndTransmissionTypeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed data for FuelTypes
            migrationBuilder.InsertData(
                table: "FuelTypes",
                columns: new[] { "Id", "Name", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Petrol", false },
                    { 2, "Diesel", false },
                    { 3, "Hybrid", false },
                    { 4, "Electric", false }
                });

            // Seed data for TransmissionTypes
            migrationBuilder.InsertData(
                table: "TransmissionTypes",
                columns: new[] { "Id", "Name", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Manual", false },
                    { 2, "Automatic", false },
                    { 3, "CVT", false },
                    { 4, "Dual-Clutch", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove FuelTypes data
            migrationBuilder.DeleteData(
                table: "FuelTypes",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });

            // Remove TransmissionTypes data
            migrationBuilder.DeleteData(
                table: "TransmissionTypes",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });
        }
    }
}