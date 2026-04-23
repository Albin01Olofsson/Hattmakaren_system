using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class decimaltillrabatt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rabatt",
                table: "Ordrar",
                type: "decimal(18,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rabatt",
                table: "Ordrar",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$bcl5J2jYDt6.OeiVJbCCL.PwzRtBOuQYeFs24.OWPWhOLyiLvzF42");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$bcl5J2jYDt6.OeiVJbCCL.PwzRtBOuQYeFs24.OWPWhOLyiLvzF42");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$bcl5J2jYDt6.OeiVJbCCL.PwzRtBOuQYeFs24.OWPWhOLyiLvzF42");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$bcl5J2jYDt6.OeiVJbCCL.PwzRtBOuQYeFs24.OWPWhOLyiLvzF42");
        }
    }
}
