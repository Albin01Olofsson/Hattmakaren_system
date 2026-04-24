using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class pris : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$7g/oxHo.y1JB95QMqB6fLO/Y.OACWOd0m8TcbD2g3Asqzg.m9c8kK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$7g/oxHo.y1JB95QMqB6fLO/Y.OACWOd0m8TcbD2g3Asqzg.m9c8kK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$7g/oxHo.y1JB95QMqB6fLO/Y.OACWOd0m8TcbD2g3Asqzg.m9c8kK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$7g/oxHo.y1JB95QMqB6fLO/Y.OACWOd0m8TcbD2g3Asqzg.m9c8kK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
