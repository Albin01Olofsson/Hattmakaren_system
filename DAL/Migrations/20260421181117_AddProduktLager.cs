using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProduktLager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Lagerantal",
                table: "Produkter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$0JfTpt1KM2vCj8Q90fTWtecX.e8h2BctNPkAEio88yUKO6a36/JOO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$0JfTpt1KM2vCj8Q90fTWtecX.e8h2BctNPkAEio88yUKO6a36/JOO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$0JfTpt1KM2vCj8Q90fTWtecX.e8h2BctNPkAEio88yUKO6a36/JOO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$0JfTpt1KM2vCj8Q90fTWtecX.e8h2BctNPkAEio88yUKO6a36/JOO");

            migrationBuilder.UpdateData(
                table: "Produkter",
                keyColumn: "ProduktID",
                keyValue: 10000001,
                column: "Lagerantal",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Produkter",
                keyColumn: "ProduktID",
                keyValue: 10000002,
                column: "Lagerantal",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Lagerantal",
                table: "Produkter",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$njFYdq7KQlMDhTBR//sUhO1NP43KQtpX0ZK7DwHWJSzphwOoC8rOm");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$njFYdq7KQlMDhTBR//sUhO1NP43KQtpX0ZK7DwHWJSzphwOoC8rOm");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$njFYdq7KQlMDhTBR//sUhO1NP43KQtpX0ZK7DwHWJSzphwOoC8rOm");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$njFYdq7KQlMDhTBR//sUhO1NP43KQtpX0ZK7DwHWJSzphwOoC8rOm");
        }
    }
}
