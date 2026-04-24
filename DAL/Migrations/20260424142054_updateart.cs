using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArtikelNr",
                table: "Artiklar",
                newName: "Namn");

            migrationBuilder.AddColumn<decimal>(
                name: "Pris",
                table: "Artiklar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pris",
                table: "Artiklar");

            migrationBuilder.RenameColumn(
                name: "Namn",
                table: "Artiklar",
                newName: "ArtikelNr");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$MHM.jmQDr7Ax1swxRkTpo.FHToMj5Ua7Dg83kuacO66GNXIqv5xqi");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$MHM.jmQDr7Ax1swxRkTpo.FHToMj5Ua7Dg83kuacO66GNXIqv5xqi");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$MHM.jmQDr7Ax1swxRkTpo.FHToMj5Ua7Dg83kuacO66GNXIqv5xqi");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$MHM.jmQDr7Ax1swxRkTpo.FHToMj5Ua7Dg83kuacO66GNXIqv5xqi");
        }
    }
}
