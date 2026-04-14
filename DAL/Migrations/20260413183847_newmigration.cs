using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class newmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rabatt",
                table: "Ordrar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000001,
                column: "Rabatt",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000002,
                column: "Rabatt",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rabatt",
                table: "Ordrar");
        }
    }
}
