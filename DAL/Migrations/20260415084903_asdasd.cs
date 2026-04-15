using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class asdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Användare",
                columns: table => new
                {
                    AnvändarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lösenord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Användare", x => x.AnvändarID);
                });

            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    KundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.KundID);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lagerantal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialBeställningar",
                columns: table => new
                {
                    MaterialBeställningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartadAvID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialBeställningar", x => x.MaterialBeställningID);
                    table.ForeignKey(
                        name: "FK_MaterialBeställningar_Användare_StartadAvID",
                        column: x => x.StartadAvID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordrar",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Färdig = table.Column<bool>(type: "bit", nullable: false),
                    Rabatt = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsSpecialbeställning = table.Column<bool>(type: "bit", nullable: false),
                    StartadAvID = table.Column<int>(type: "int", nullable: false),
                    KundID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordrar", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Ordrar_Användare_StartadAvID",
                        column: x => x.StartadAvID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordrar_Kunder_KundID",
                        column: x => x.KundID,
                        principalTable: "Kunder",
                        principalColumn: "KundID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialMaterialBeställning",
                columns: table => new
                {
                    MaterialBeställningID = table.Column<int>(type: "int", nullable: false),
                    MaterialListaMaterialID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialMaterialBeställning", x => new { x.MaterialBeställningID, x.MaterialListaMaterialID });
                    table.ForeignKey(
                        name: "FK_MaterialMaterialBeställning_MaterialBeställningar_MaterialBeställningID",
                        column: x => x.MaterialBeställningID,
                        principalTable: "MaterialBeställningar",
                        principalColumn: "MaterialBeställningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialMaterialBeställning_Material_MaterialListaMaterialID",
                        column: x => x.MaterialListaMaterialID,
                        principalTable: "Material",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produkter",
                columns: table => new
                {
                    ProduktID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Färdig = table.Column<bool>(type: "bit", nullable: false),
                    Storlek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    TillverkadAVID = table.Column<int>(type: "int", nullable: false),
                    ProduktTyp = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ArtikelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kategori = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lagerantal = table.Column<int>(type: "int", nullable: true),
                    BildURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkter", x => x.ProduktID);
                    table.ForeignKey(
                        name: "FK_Produkter_Användare_TillverkadAVID",
                        column: x => x.TillverkadAVID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produkter_Ordrar_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Ordrar",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialProdukt",
                columns: table => new
                {
                    MaterialListaMaterialID = table.Column<int>(type: "int", nullable: false),
                    ProduktID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialProdukt", x => new { x.MaterialListaMaterialID, x.ProduktID });
                    table.ForeignKey(
                        name: "FK_MaterialProdukt_Material_MaterialListaMaterialID",
                        column: x => x.MaterialListaMaterialID,
                        principalTable: "Material",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialProdukt_Produkter_ProduktID",
                        column: x => x.ProduktID,
                        principalTable: "Produkter",
                        principalColumn: "ProduktID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Användare",
                columns: new[] { "AnvändarID", "Email", "IsAdmin", "Lösenord", "Namn", "Telefon" },
                values: new object[,]
                {
                    { 1, "ottoHattman@hotmail.com", true, "$2a$11$P1LRCy7qqKenR/.LHJ/gFuS/LZOnlxCi84KZWlFfZJFv0r9gbocJO", "Otto", "07085652321" },
                    { 2, "JudithHattman@hotmail.com", false, "$2a$11$P1LRCy7qqKenR/.LHJ/gFuS/LZOnlxCi84KZWlFfZJFv0r9gbocJO", "Judith", "0727639856" },
                    { 3, "MillieHattman@hotmail.com", false, "$2a$11$P1LRCy7qqKenR/.LHJ/gFuS/LZOnlxCi84KZWlFfZJFv0r9gbocJO", "Millie", "0709825533" },
                    { 4, "HerbertHattman@hotmail.com", false, "$2a$11$P1LRCy7qqKenR/.LHJ/gFuS/LZOnlxCi84KZWlFfZJFv0r9gbocJO", "Herbert", "0705512322" }
                });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "KundID", "Adress", "Email", "Namn", "Telefon" },
                values: new object[,]
                {
                    { 1001, "Kullstigen 78", "Per.Larsson@hotmail.com", "Per Larsson", "076312129" },
                    { 1002, "Milvägen 1", "Eva.Milen@hotmail.com", "Eva Von Milen", "0727728432" },
                    { 1003, "Fjordaberg 51", "yvonne.fjord@hotmail.com", "Yvonne Fjord", "0702127345" },
                    { 1004, "Javatorget 23", "ahmed.khan@hotmail.com", "Ahmed Khan", "070123382" },
                    { 1005, "Tetornet 3", "jasmin.barsk@hotmail.com", "Jasmin Barsk", "0702427373" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialID", "Beskrivning", "Lagerantal", "Namn", "Pris", "Typ" },
                values: new object[,]
                {
                    { 100001, "Inte filt man sover med", 23, "Filt", 54m, "Tyg" },
                    { 100002, "100% obesprutat bomull", 52, "Bomull", 34m, "Tyg" },
                    { 100003, "1.2 mm svar syträd av silikon och polyester", 2, "Svart tråd", 28m, "Tråd" }
                });

            migrationBuilder.InsertData(
                table: "MaterialBeställningar",
                columns: new[] { "MaterialBeställningID", "StartadAvID", "TotalPris" },
                values: new object[,]
                {
                    { 1000001, 1, 1890m },
                    { 1000002, 2, 769m },
                    { 1000003, 1, 3419m }
                });

            migrationBuilder.InsertData(
                table: "Ordrar",
                columns: new[] { "OrderID", "Datum", "Färdig", "IsSpecialbeställning", "KundID", "Pris", "Rabatt", "StartadAvID" },
                values: new object[,]
                {
                    { 100000001, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 1001, 1299m, 0m, 1 },
                    { 100000002, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1002, 1099m, 0m, 1 },
                    { 100000003, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1003, 299m, 0m, 1 },
                    { 100000004, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 1004, 2399m, 0m, 1 },
                    { 100000005, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1005, 779m, 0m, 1 },
                    { 100000006, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1001, 949m, 0m, 2 },
                    { 100000007, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 1002, 1049m, 0m, 2 },
                    { 100000008, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1003, 749m, 0m, 2 },
                    { 100000009, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1004, 999m, 0m, 2 },
                    { 100000010, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1004, 899m, 0m, 2 },
                    { 100000011, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1005, 1099m, 0m, 2 },
                    { 100000012, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1001, 2019m, 0m, 3 },
                    { 100000013, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, 1002, 1829m, 0m, 3 },
                    { 100000014, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1003, 599m, 0m, 3 },
                    { 100000015, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1004, 899m, 0m, 3 },
                    { 100000016, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1005, 1299m, 0m, 3 },
                    { 100000017, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1001, 499m, 0m, 4 },
                    { 100000018, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1002, 499m, 0m, 4 },
                    { 100000019, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1003, 499m, 0m, 4 },
                    { 100000020, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1004, 499m, 0m, 4 },
                    { 100000021, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, 1005, 499m, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Produkter",
                columns: new[] { "ProduktID", "Färdig", "OrderID", "ProduktTyp", "Storlek", "TillverkadAVID", "namn", "pris" },
                values: new object[,]
                {
                    { 10000001, false, 100000001, "Produkt", "M", 1, "Filt hatt", 1099m },
                    { 10000002, false, 100000002, "Produkt", "M", 2, "Siden hatt", 949m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBeställningar_StartadAvID",
                table: "MaterialBeställningar",
                column: "StartadAvID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMaterialBeställning_MaterialListaMaterialID",
                table: "MaterialMaterialBeställning",
                column: "MaterialListaMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialProdukt_ProduktID",
                table: "MaterialProdukt",
                column: "ProduktID");

            migrationBuilder.CreateIndex(
                name: "IX_Ordrar_KundID",
                table: "Ordrar",
                column: "KundID");

            migrationBuilder.CreateIndex(
                name: "IX_Ordrar_StartadAvID",
                table: "Ordrar",
                column: "StartadAvID");

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_OrderID",
                table: "Produkter",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_TillverkadAVID",
                table: "Produkter",
                column: "TillverkadAVID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialMaterialBeställning");

            migrationBuilder.DropTable(
                name: "MaterialProdukt");

            migrationBuilder.DropTable(
                name: "MaterialBeställningar");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Produkter");

            migrationBuilder.DropTable(
                name: "Ordrar");

            migrationBuilder.DropTable(
                name: "Användare");

            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
