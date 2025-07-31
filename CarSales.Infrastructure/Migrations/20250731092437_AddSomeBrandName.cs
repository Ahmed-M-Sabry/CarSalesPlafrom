using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSales.Infrastructure.Migrations
{
    public partial class AddSomeBrandName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Toyota", false },
                    { 2, "Hyundai", false },
                    { 3, "Nissan", false },
                    { 4, "Honda", false },
                    { 5, "Kia", false },
                    { 6, "Ford", false },
                    { 7, "Chevrolet", false },
                    { 8, "BMW", false },
                    { 9, "Mercedes-Benz", false },
                    { 10, "Volkswagen", false },
                    { 11, "Mitsubishi", false },
                    { 12, "Peugeot", false },
                    { 13, "Renault", false },
                    { 14, "Suzuki", false },
                    { 15, "Fiat", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        }
    }
}