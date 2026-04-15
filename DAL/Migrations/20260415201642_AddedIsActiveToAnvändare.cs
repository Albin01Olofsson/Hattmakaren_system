using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveToAnvändare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Användare",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                columns: new[] { "IsActive", "Lösenord" },
                values: new object[] { true, "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi" });

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                columns: new[] { "IsActive", "Lösenord" },
                values: new object[] { true, "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi" });

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                columns: new[] { "IsActive", "Lösenord" },
                values: new object[] { true, "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi" });

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                columns: new[] { "IsActive", "Lösenord" },
                values: new object[] { true, "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Användare");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$X0xWYhvE0n8gNbnd8mkjv.0LVh15QTI0CEN02JwnQCIkOZZou8XZ6");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$X0xWYhvE0n8gNbnd8mkjv.0LVh15QTI0CEN02JwnQCIkOZZou8XZ6");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$X0xWYhvE0n8gNbnd8mkjv.0LVh15QTI0CEN02JwnQCIkOZZou8XZ6");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$X0xWYhvE0n8gNbnd8mkjv.0LVh15QTI0CEN02JwnQCIkOZZou8XZ6");
        }
    }
}
