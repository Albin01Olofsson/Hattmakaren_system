using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
<<<<<<<< HEAD:DAL/Migrations/20260427114445_Prispåfrakt.cs
    public partial class Prispåfrakt : Migration
========
    public partial class _1 : Migration
>>>>>>>> master:DAL/Migrations/20260427095112_1.cs
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
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FöretagsKund = table.Column<bool>(type: "bit", nullable: false),
                    Land = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    MåttTyp = table.Column<int>(type: "int", nullable: false),
                    Lagerantal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialID);
                });

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
                name: "MaterialBeställningar",
                columns: table => new
                {
                    MaterialBeställningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Leverantör = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Produkter",
                columns: table => new
                {
                    ProduktID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Färdig = table.Column<bool>(type: "bit", nullable: false),
                    Storlek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HattTyp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modell = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Färg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decoration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TillverkadAVID = table.Column<int>(type: "int", nullable: false),
                    Lagerantal = table.Column<int>(type: "int", nullable: false),
                    ProduktTyp = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ArtikelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kategori = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Ordrar",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Varukod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Moms = table.Column<double>(type: "float", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Färdig = table.Column<bool>(type: "bit", nullable: false),
                    Rabatt = table.Column<decimal>(type: "decimal(18,2)", precision: 5, scale: 2, nullable: false),
                    IsSpecialbeställning = table.Column<bool>(type: "bit", nullable: false),
                    IsPrio = table.Column<bool>(type: "bit", nullable: false),
                    StartadAvID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FörväntadTillverkningsTid = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "BestallningsRader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Antal = table.Column<int>(type: "int", nullable: false),
                    MaterialBeställningID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestallningsRader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestallningsRader_MaterialBeställningar_MaterialBeställningID",
                        column: x => x.MaterialBeställningID,
                        principalTable: "MaterialBeställningar",
                        principalColumn: "MaterialBeställningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestallningsRader_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Frakt",
                columns: table => new
                {
                    Sändningsnummer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KolliId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transportör = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frakt", x => x.Sändningsnummer);
                    table.ForeignKey(
                        name: "FK_Frakt_Ordrar_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Ordrar",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRader",
                columns: table => new
                {
                    OrderRadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProduktID = table.Column<int>(type: "int", nullable: false),
                    Antal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRader", x => x.OrderRadID);
                    table.ForeignKey(
                        name: "FK_OrderRader_Ordrar_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Ordrar",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRader_Produkter_ProduktID",
                        column: x => x.ProduktID,
                        principalTable: "Produkter",
                        principalColumn: "ProduktID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Planeringar",
                columns: table => new
                {
                    PlaneringsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlutTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaneringsNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnvändarID = table.Column<int>(type: "int", nullable: false),
                    OrderRadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planeringar", x => x.PlaneringsID);
                    table.ForeignKey(
                        name: "FK_Planeringar_Användare_AnvändarID",
                        column: x => x.AnvändarID,
                        principalTable: "Användare",
                        principalColumn: "AnvändarID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planeringar_OrderRader_OrderRadID",
                        column: x => x.OrderRadID,
                        principalTable: "OrderRader",
                        principalColumn: "OrderRadID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Användare",
                columns: new[] { "AnvändarID", "Email", "IsActive", "IsAdmin", "Lösenord", "Namn", "Telefon" },
                values: new object[,]
                {
<<<<<<<< HEAD:DAL/Migrations/20260427114445_Prispåfrakt.cs
                    { 1, "ottoHattman@hotmail.com", true, true, "$2a$11$CkS1Q/y9Fx01YNG0isQlC.4KOsPZhT0K84S4Q7n70CQP6MyH.8BQC", "Otto", "07085652321" },
                    { 2, "JudithHattman@hotmail.com", true, false, "$2a$11$CkS1Q/y9Fx01YNG0isQlC.4KOsPZhT0K84S4Q7n70CQP6MyH.8BQC", "Judith", "0727639856" },
                    { 3, "MillieHattman@hotmail.com", true, false, "$2a$11$CkS1Q/y9Fx01YNG0isQlC.4KOsPZhT0K84S4Q7n70CQP6MyH.8BQC", "Millie", "0709825533" },
                    { 4, "HerbertHattman@hotmail.com", true, false, "$2a$11$CkS1Q/y9Fx01YNG0isQlC.4KOsPZhT0K84S4Q7n70CQP6MyH.8BQC", "Herbert", "0705512322" }
========
                    { 1, "ottoHattman@hotmail.com", true, true, "$2a$11$Vy1q.yCoBl/iyv8FppqgG.YywbGBrsFKJ.WM253C1B5Z4xUCzAIZe", "Otto", "07085652321" },
                    { 2, "JudithHattman@hotmail.com", true, false, "$2a$11$Vy1q.yCoBl/iyv8FppqgG.YywbGBrsFKJ.WM253C1B5Z4xUCzAIZe", "Judith", "0727639856" },
                    { 3, "MillieHattman@hotmail.com", true, false, "$2a$11$Vy1q.yCoBl/iyv8FppqgG.YywbGBrsFKJ.WM253C1B5Z4xUCzAIZe", "Millie", "0709825533" },
                    { 4, "HerbertHattman@hotmail.com", true, false, "$2a$11$Vy1q.yCoBl/iyv8FppqgG.YywbGBrsFKJ.WM253C1B5Z4xUCzAIZe", "Herbert", "0705512322" }
>>>>>>>> master:DAL/Migrations/20260427095112_1.cs
                });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "KundID", "Adress", "Email", "FöretagsKund", "Land", "Namn", "Stad", "Telefon" },
                values: new object[,]
                {
                    { 1001, "Kullstigen 78", "Per.Larsson@hotmail.com", true, "Sverige", "Per Larsson", "Stockholm", "076312129" },
                    { 1002, "Milvägen 1", "Eva.Milen@hotmail.com", true, "Sverige", "Eva Von Milen", "Stockholm", "0727728432" },
                    { 1003, "Fjordaberg 51", "yvonne.fjord@hotmail.com", false, "Finland", "Yvonne Fjord", "Helsingfors", "0702127345" },
                    { 1004, "Javatorget 23", "ahmed.khan@hotmail.com", false, "Sverige", "Ahmed Khan", "Örebro", "070123382" },
                    { 1005, "Tetornet 3", "jasmin.barsk@hotmail.com", false, "Sverige", "Jasmin Barsk", "Stockholm", "0702427373" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialID", "Beskrivning", "Lagerantal", "MåttTyp", "Namn", "Pris" },
                values: new object[,]
                {
                    { 100001, "Inte filt man sover med", 23, 0, "Filt", 54m },
                    { 100002, "100% obesprutat bomull", 52, 0, "Bomull", 34m },
                    { 100003, "1.2 mm svar syträd av silikon och polyester", 2, 0, "Svart tråd", 28m },
                    { 100004, "Tunt siden till foder och detaljer", 18, 0, "Siden", 89m },
                    { 100005, "Brunt läderband till hattdekoration", 11, 0, "Läderband", 45m }
                });

            migrationBuilder.InsertData(
                table: "MaterialBeställningar",
                columns: new[] { "MaterialBeställningID", "Datum", "Leverantör", "StartadAvID", "TotalPris" },
                values: new object[,]
                {
                    { 1000001, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kung AB", 1, 1890m },
                    { 1000002, new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nordic Textile", 2, 769m },
                    { 1000003, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Skrädderi Grossisten", 1, 3419m }
                });

            migrationBuilder.InsertData(
                table: "Ordrar",
                columns: new[] { "OrderID", "Datum", "Färdig", "FörväntadTillverkningsTid", "IsPrio", "IsSpecialbeställning", "KundID", "Moms", "Pris", "Rabatt", "StartadAvID", "Status", "Varukod" },
                values: new object[,]
                {
                    { 100000001, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1001, null, 1299m, 0m, 1, "Ej påbörjat", "" },
                    { 100000002, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1002, null, 1099m, 0m, 1, "Ej påbörjat", "" },
                    { 100000003, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1003, null, 299m, 0m, 1, "Ej påbörjat", "" },
                    { 100000004, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1004, null, 2399m, 0m, 1, "Ej påbörjat", "" },
                    { 100000005, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1005, null, 779m, 0m, 1, "Ej påbörjat", "" },
                    { 100000006, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1001, null, 949m, 0m, 2, "Ej påbörjat", "" },
                    { 100000007, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1002, null, 1049m, 0m, 2, "Ej påbörjat", "" },
                    { 100000008, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1003, null, 749m, 0m, 2, "Ej påbörjat", "" },
                    { 100000009, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1004, null, 999m, 0m, 2, "Ej påbörjat", "" },
                    { 100000010, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1004, null, 899m, 0m, 2, "Ej påbörjat", "" },
                    { 100000011, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1005, null, 1099m, 0m, 2, "Ej påbörjat", "" },
                    { 100000012, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1001, null, 2019m, 0m, 3, "Ej påbörjat", "" },
                    { 100000013, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1002, null, 1829m, 0m, 3, "Ej påbörjat", "" },
                    { 100000014, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1003, null, 599m, 0m, 3, "Ej påbörjat", "" },
                    { 100000015, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1004, null, 899m, 0m, 3, "Ej påbörjat", "" },
                    { 100000016, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1005, null, 1299m, 0m, 3, "Ej påbörjat", "" },
                    { 100000017, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1001, null, 499m, 0m, 4, "Ej påbörjat", "" },
                    { 100000018, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1002, null, 499m, 0m, 4, "Ej påbörjat", "" },
                    { 100000019, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1003, null, 499m, 0m, 4, "Ej påbörjat", "" },
                    { 100000020, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1004, null, 499m, 0m, 4, "Ej påbörjat", "" },
                    { 100000021, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 1005, null, 499m, 0m, 4, "Ej påbörjat", "" }
                });

            migrationBuilder.InsertData(
                table: "Produkter",
                columns: new[] { "ProduktID", "ArtikelID", "Decoration", "Färdig", "Färg", "HattTyp", "Kategori", "Lagerantal", "Modell", "Namn", "Pris", "ProduktTyp", "Storlek", "TillverkadAVID" },
                values: new object[,]
                {
                    { 10000001, "LP0001", "Läderband", true, "Svart", "Fedora", "Hatt", 7, "Klassisk", "Filt hatt", 1099m, "Lagerförd", "M", 1 },
                    { 10000002, "LP0002", "Svart tråd", true, "Blå", "Keps", "Keps", 5, "Siden", "Siden keps", 949m, "Lagerförd", "M", 2 },
                    { 10000003, "LP0003", "Bomullsband", true, "Naturvit", "Panama", "Hatt", 4, "Sommar", "Sommarhatt", 799m, "Lagerförd", "L", 3 }
                });

            migrationBuilder.InsertData(
                table: "Produkter",
                columns: new[] { "ProduktID", "Beskrivning", "BildURL", "Decoration", "Färdig", "Färg", "HattTyp", "Lagerantal", "Modell", "Namn", "Pris", "ProduktTyp", "Storlek", "TillverkadAVID" },
                values: new object[] { 10000004, "Specialbeställd bröllopshatt med sidenrosett", "", "Sidenrosett", false, "Creme", "Fascinator", 0, "Bröllop", "Bröllopshatt", 1899m, "Special", "S", 4 });

            migrationBuilder.InsertData(
                table: "BestallningsRader",
                columns: new[] { "Id", "Antal", "MaterialBeställningID", "MaterialId" },
                values: new object[,]
                {
                    { 10001, 20, 1000001, 100001 },
                    { 10002, 10, 1000001, 100003 },
                    { 10003, 30, 1000002, 100002 },
                    { 10004, 12, 1000002, 100004 },
                    { 10005, 35, 1000003, 100001 },
                    { 10006, 15, 1000003, 100005 }
                });

            migrationBuilder.InsertData(
                table: "MaterialMaterialBeställning",
                columns: new[] { "MaterialBeställningID", "MaterialListaMaterialID" },
                values: new object[,]
                {
                    { 1000001, 100001 },
                    { 1000001, 100003 },
                    { 1000002, 100002 },
                    { 1000002, 100004 },
                    { 1000003, 100001 },
                    { 1000003, 100005 }
                });

            migrationBuilder.InsertData(
                table: "MaterialProdukt",
                columns: new[] { "MaterialListaMaterialID", "ProduktID" },
                values: new object[,]
                {
                    { 100001, 10000001 },
                    { 100003, 10000001 },
                    { 100005, 10000001 },
                    { 100004, 10000002 },
                    { 100003, 10000002 },
                    { 100002, 10000003 },
                    { 100005, 10000003 },
                    { 100004, 10000004 },
                    { 100002, 10000004 },
                    { 100003, 10000004 }
                });

            migrationBuilder.InsertData(
                table: "OrderRader",
                columns: new[] { "OrderRadID", "Antal", "OrderID", "ProduktID" },
                values: new object[,]
                {
                    { 20001, 1, 100000001, 10000004 },
                    { 20002, 1, 100000002, 10000001 },
                    { 20003, 1, 100000003, 10000003 },
                    { 20004, 1, 100000004, 10000004 },
                    { 20005, 1, 100000005, 10000002 },
                    { 20006, 1, 100000006, 10000002 },
                    { 20007, 1, 100000007, 10000001 },
                    { 20008, 2, 100000008, 10000003 },
                    { 20009, 1, 100000009, 10000001 },
                    { 20010, 1, 100000010, 10000002 },
                    { 20011, 1, 100000011, 10000001 },
                    { 20012, 1, 100000012, 10000004 },
                    { 20013, 1, 100000013, 10000004 },
                    { 20014, 1, 100000014, 10000003 },
                    { 20015, 1, 100000015, 10000002 },
                    { 20016, 1, 100000016, 10000004 },
                    { 20017, 1, 100000017, 10000003 },
                    { 20018, 1, 100000018, 10000001 },
                    { 20019, 1, 100000019, 10000002 },
                    { 20020, 1, 100000020, 10000003 },
                    { 20021, 1, 100000021, 10000001 }
                });

            migrationBuilder.InsertData(
                table: "Reklamationer",
                columns: new[] { "ReklamationID", "Atgard", "AvslutadDatum", "Beskrivning", "KundID", "OrderID", "Orsak", "ProduktID", "SkapadAvID", "SkapadDatum", "Status" },
                values: new object[,]
                {
                    { 30001, "Justering", null, "Kunden önskar justering av passform.", 1003, 100000008, "Fel storlek", 10000003, 1, new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ny" },
                    { 30002, "Reparation", null, "Sidenrosetten behöver bytas till ljusare nyans.", 1002, 100000013, "Fel färg", 10000004, 2, new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Under behandling" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aktiviteter_SkapadAvID",
                table: "Aktiviteter",
                column: "SkapadAvID");

            migrationBuilder.CreateIndex(
                name: "IX_AnvändarAktiviteter_DeltarIAktiviteterAktivitetID",
                table: "AnvändarAktiviteter",
                column: "DeltarIAktiviteterAktivitetID");

            migrationBuilder.CreateIndex(
                name: "IX_BestallningsRader_MaterialBeställningID",
                table: "BestallningsRader",
                column: "MaterialBeställningID");

            migrationBuilder.CreateIndex(
                name: "IX_BestallningsRader_MaterialId",
                table: "BestallningsRader",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Frakt_OrderID",
                table: "Frakt",
                column: "OrderID");

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
                name: "IX_OrderRader_OrderID",
                table: "OrderRader",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRader_ProduktID",
                table: "OrderRader",
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
                name: "IX_Planeringar_AnvändarID",
                table: "Planeringar",
                column: "AnvändarID");

            migrationBuilder.CreateIndex(
                name: "IX_Planeringar_OrderRadID",
                table: "Planeringar",
                column: "OrderRadID");

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_TillverkadAVID",
                table: "Produkter",
                column: "TillverkadAVID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnvändarAktiviteter");

            migrationBuilder.DropTable(
                name: "BestallningsRader");

            migrationBuilder.DropTable(
                name: "Frakt");

            migrationBuilder.DropTable(
                name: "MaterialMaterialBeställning");

            migrationBuilder.DropTable(
                name: "MaterialProdukt");

            migrationBuilder.DropTable(
                name: "Planeringar");

            migrationBuilder.DropTable(
                name: "Reklamationer");

            migrationBuilder.DropTable(
                name: "Aktiviteter");

            migrationBuilder.DropTable(
                name: "MaterialBeställningar");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "OrderRader");

            migrationBuilder.DropTable(
                name: "Ordrar");

            migrationBuilder.DropTable(
                name: "Produkter");

            migrationBuilder.DropTable(
                name: "Kunder");

            migrationBuilder.DropTable(
                name: "Användare");
        }
    }
}
