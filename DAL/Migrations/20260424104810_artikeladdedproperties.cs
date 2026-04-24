using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class artikeladdedproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Antal",
                table: "Artiklar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Decoration",
                table: "Artiklar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Färg",
                table: "Artiklar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HattTyp",
                table: "Artiklar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Modell",
                table: "Artiklar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Namn",
                table: "Artiklar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                value: "$2a$11$yTB2pUdys8lFxVuhMsm1Ke2U5Mucr0JiBfEs7isI5h59/.bjkQajS");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$yTB2pUdys8lFxVuhMsm1Ke2U5Mucr0JiBfEs7isI5h59/.bjkQajS");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$yTB2pUdys8lFxVuhMsm1Ke2U5Mucr0JiBfEs7isI5h59/.bjkQajS");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$yTB2pUdys8lFxVuhMsm1Ke2U5Mucr0JiBfEs7isI5h59/.bjkQajS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Antal",
                table: "Artiklar");

            migrationBuilder.DropColumn(
                name: "Decoration",
                table: "Artiklar");

            migrationBuilder.DropColumn(
                name: "Färg",
                table: "Artiklar");

            migrationBuilder.DropColumn(
                name: "HattTyp",
                table: "Artiklar");

            migrationBuilder.DropColumn(
                name: "Modell",
                table: "Artiklar");

            migrationBuilder.DropColumn(
                name: "Namn",
                table: "Artiklar");

            migrationBuilder.DropColumn(
                name: "Pris",
                table: "Artiklar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$9m2U4.E4Yw2ysaNMqOPLYOoLTF5EEuXEb0aqinxZ/gcddKcYQDdXS");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$9m2U4.E4Yw2ysaNMqOPLYOoLTF5EEuXEb0aqinxZ/gcddKcYQDdXS");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$9m2U4.E4Yw2ysaNMqOPLYOoLTF5EEuXEb0aqinxZ/gcddKcYQDdXS");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$9m2U4.E4Yw2ysaNMqOPLYOoLTF5EEuXEb0aqinxZ/gcddKcYQDdXS");
        }
    }
}
