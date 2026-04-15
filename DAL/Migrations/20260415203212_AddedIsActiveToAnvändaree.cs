using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveToAnvändaree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$Cv.Aa/ZDnxqTt15i0vqR9elj35zG.giRlPRe4UuNDfcV3uKzYPOMK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$Cv.Aa/ZDnxqTt15i0vqR9elj35zG.giRlPRe4UuNDfcV3uKzYPOMK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$Cv.Aa/ZDnxqTt15i0vqR9elj35zG.giRlPRe4UuNDfcV3uKzYPOMK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$Cv.Aa/ZDnxqTt15i0vqR9elj35zG.giRlPRe4UuNDfcV3uKzYPOMK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$7QmZogPmY2nS2EpYZqgj8enwMv5kCxSIvHrD0WM1C5x7MtDxAWIIi");
        }
    }
}
