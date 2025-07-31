using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSales.Infrastructure.Migrations
{
    public partial class AddModelData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Name", "IsDeleted", "BrandId" },
                values: new object[,]
                {
                    // Toyota (BrandId = 1)
                    { 1, "Corolla", false, 1 },
                    { 2, "Camry", false, 1 },
                    { 3, "RAV4", false, 1 },
                    // Hyundai (BrandId = 2)
                    { 4, "Elantra", false, 2 },
                    { 5, "Tucson", false, 2 },
                    { 6, "Sonata", false, 2 },
                    // Nissan (BrandId = 3)
                    { 7, "Sunny", false, 3 },
                    { 8, "Sentra", false, 3 },
                    { 9, "X-Trail", false, 3 },
                    // Honda (BrandId = 4)
                    { 10, "Civic", false, 4 },
                    { 11, "Accord", false, 4 },
                    { 12, "CR-V", false, 4 },
                    // Kia (BrandId = 5)
                    { 13, "Cerato", false, 5 },
                    { 14, "Sportage", false, 5 },
                    { 15, "Optima", false, 5 },
                    // Ford (BrandId = 6)
                    { 16, "Focus", false, 6 },
                    { 17, "Fiesta", false, 6 },
                    { 18, "Escape", false, 6 },
                    // Chevrolet (BrandId = 7)
                    { 19, "Optra", false, 7 },
                    { 20, "Cruze", false, 7 },
                    { 21, "Malibu", false, 7 },
                    // BMW (BrandId = 8)
                    { 22, "3 Series", false, 8 },
                    { 23, "5 Series", false, 8 },
                    { 24, "X5", false, 8 },
                    // Mercedes-Benz (BrandId = 9)
                    { 25, "C-Class", false, 9 },
                    { 26, "E-Class", false, 9 },
                    { 27, "GLC", false, 9 },
                    // Volkswagen (BrandId = 10)
                    { 28, "Jetta", false, 10 },
                    { 29, "Passat", false, 10 },
                    { 30, "Tiguan", false, 10 },
                    // Mitsubishi (BrandId = 11)
                    { 31, "Lancer", false, 11 },
                    { 32, "Pajero", false, 11 },
                    { 33, "Outlander", false, 11 },
                    // Peugeot (BrandId = 12)
                    { 34, "301", false, 12 },
                    { 35, "508", false, 12 },
                    { 36, "3008", false, 12 },
                    // Renault (BrandId = 13)
                    { 37, "Logan", false, 13 },
                    { 38, "Megane", false, 13 },
                    { 39, "Captur", false, 13 },
                    // Suzuki (BrandId = 14)
                    { 40, "Swift", false, 14 },
                    { 41, "Vitara", false, 14 },
                    { 42, "Ertiga", false, 14 },
                    // Fiat (BrandId = 15)
                    { 43, "Tipo", false, 15 },
                    { 44, "500", false, 15 },
                    { 45, "Panda", false, 15 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                    21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                    31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                    41, 42, 43, 44, 45
                });
        }
    }
}