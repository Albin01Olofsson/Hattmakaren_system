using System;
using DAL;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DBcontext))]
    [Migration("20260421115400_AddReklamationer")]
    public partial class AddReklamationer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reklamationer",
                columns: table => new
                {
                    ReklamationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProduktID = table.Column<int>(type: "int", nullable: true),
                    KundID = table.Column<int>(type: "int", nullable: false),
                    Orsak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atgard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkapadDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvslutadDatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SkapadAvID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reklamationer", x => x.ReklamationID);
                    table.ForeignKey(
                        name: "FK_Reklamationer_Användare_SkapadAvID",
                        column: x => x.SkapadAvID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reklamationer_Kunder_KundID",
                        column: x => x.KundID,
                        principalTable: "Kunder",
                        principalColumn: "KundID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reklamationer_Ordrar_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Ordrar",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reklamationer_Produkter_ProduktID",
                        column: x => x.ProduktID,
                        principalTable: "Produkter",
                        principalColumn: "ProduktID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reklamationer_KundID",
                table: "Reklamationer",
                column: "KundID");

            migrationBuilder.CreateIndex(
                name: "IX_Reklamationer_OrderID",
                table: "Reklamationer",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reklamationer_ProduktID",
                table: "Reklamationer",
                column: "ProduktID");

            migrationBuilder.CreateIndex(
                name: "IX_Reklamationer_SkapadAvID",
                table: "Reklamationer",
                column: "SkapadAvID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reklamationer");
        }
    }
}
