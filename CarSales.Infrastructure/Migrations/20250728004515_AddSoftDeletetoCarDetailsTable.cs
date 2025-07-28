using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeletetoCarDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TransmissionTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Models",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FuelTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TransmissionTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FuelTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Brands");
        }
    }
}
