using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class almostnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Moms",
                table: "Ordrar",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Aktiviteter",
                columns: table => new
                {
                    AktivitetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlutTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SkapadAvID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktiviteter", x => x.AktivitetID);
                    table.ForeignKey(
                        name: "FK_Aktiviteter_Användare_SkapadAvID",
                        column: x => x.SkapadAvID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnvändarAktiviteter",
                columns: table => new
                {
                    DeltagareAnvändarID = table.Column<int>(type: "int", nullable: false),
                    DeltarIAktiviteterAktivitetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnvändarAktiviteter", x => new { x.DeltagareAnvändarID, x.DeltarIAktiviteterAktivitetID });
                    table.ForeignKey(
                        name: "FK_AnvändarAktiviteter_Aktiviteter_DeltarIAktiviteterAktivitetID",
                        column: x => x.DeltarIAktiviteterAktivitetID,
                        principalTable: "Aktiviteter",
                        principalColumn: "AktivitetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnvändarAktiviteter_Användare_DeltagareAnvändarID",
                        column: x => x.DeltagareAnvändarID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$QwJoMS2lWx8Fd7hor/EO9eCeEyBQ3Os6QxLxM2Vbt.DBAzug5lLvG");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$QwJoMS2lWx8Fd7hor/EO9eCeEyBQ3Os6QxLxM2Vbt.DBAzug5lLvG");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$QwJoMS2lWx8Fd7hor/EO9eCeEyBQ3Os6QxLxM2Vbt.DBAzug5lLvG");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$QwJoMS2lWx8Fd7hor/EO9eCeEyBQ3Os6QxLxM2Vbt.DBAzug5lLvG");

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000001,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000002,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000003,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000004,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000005,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000006,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000007,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000008,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000009,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000010,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000011,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000012,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000013,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000014,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000015,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000016,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000017,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000018,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000019,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000020,
                column: "Moms",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000021,
                column: "Moms",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Aktiviteter_SkapadAvID",
                table: "Aktiviteter",
                column: "SkapadAvID");

            migrationBuilder.CreateIndex(
                name: "IX_AnvändarAktiviteter_DeltarIAktiviteterAktivitetID",
                table: "AnvändarAktiviteter",
                column: "DeltarIAktiviteterAktivitetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnvändarAktiviteter");

            migrationBuilder.DropTable(
                name: "Aktiviteter");

            migrationBuilder.DropColumn(
                name: "Moms",
                table: "Ordrar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$5OdQw6Y5Dc4is4BunF4H4uZ.gf/SBYP3FjFrgGBO.DD1xkaIfbXDC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$5OdQw6Y5Dc4is4BunF4H4uZ.gf/SBYP3FjFrgGBO.DD1xkaIfbXDC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$5OdQw6Y5Dc4is4BunF4H4uZ.gf/SBYP3FjFrgGBO.DD1xkaIfbXDC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$5OdQw6Y5Dc4is4BunF4H4uZ.gf/SBYP3FjFrgGBO.DD1xkaIfbXDC");
        }
    }
}
