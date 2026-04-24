using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateartpris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$78j489XKK6ej01zdhAWI6ud3x5znwDMiZfD7ZWOcqgiT3XamfM44.");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$78j489XKK6ej01zdhAWI6ud3x5znwDMiZfD7ZWOcqgiT3XamfM44.");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$78j489XKK6ej01zdhAWI6ud3x5znwDMiZfD7ZWOcqgiT3XamfM44.");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$78j489XKK6ej01zdhAWI6ud3x5znwDMiZfD7ZWOcqgiT3XamfM44.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$19TGd8/Yzw4S2/eERGJW/..QYk9C40DpybwUIJMilpvApykS.SkKW");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$19TGd8/Yzw4S2/eERGJW/..QYk9C40DpybwUIJMilpvApykS.SkKW");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$19TGd8/Yzw4S2/eERGJW/..QYk9C40DpybwUIJMilpvApykS.SkKW");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$19TGd8/Yzw4S2/eERGJW/..QYk9C40DpybwUIJMilpvApykS.SkKW");
        }
    }
}
