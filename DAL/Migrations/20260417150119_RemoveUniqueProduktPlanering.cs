using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueProduktPlanering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Planeringar_ProduktID",
                table: "Planeringar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$zGqzfGaFl.cTN9lSXgDrHOwP0htkPjgPnFkGBEbPH0U4wJzr/cjYK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$zGqzfGaFl.cTN9lSXgDrHOwP0htkPjgPnFkGBEbPH0U4wJzr/cjYK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$zGqzfGaFl.cTN9lSXgDrHOwP0htkPjgPnFkGBEbPH0U4wJzr/cjYK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$zGqzfGaFl.cTN9lSXgDrHOwP0htkPjgPnFkGBEbPH0U4wJzr/cjYK");

            migrationBuilder.CreateIndex(
                name: "IX_Planeringar_ProduktID",
                table: "Planeringar",
                column: "ProduktID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Planeringar_ProduktID",
                table: "Planeringar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.CreateIndex(
                name: "IX_Planeringar_ProduktID",
                table: "Planeringar",
                column: "ProduktID",
                unique: true);
        }
    }
}
