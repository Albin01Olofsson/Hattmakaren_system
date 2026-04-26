using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrderExamples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                SET IDENTITY_INSERT [Ordrar] ON;

                INSERT INTO [Ordrar] ([OrderID], [Varukod], [Pris], [Moms], [Datum], [Färdig], [Rabatt], [IsSpecialbeställning], [IsPrio], [StartadAvID], [Status], [FörväntadTillverkningsTid], [KundID])
                SELECT v.[OrderID], v.[Varukod], v.[Pris], v.[Moms], v.[Datum], v.[Färdig], v.[Rabatt], v.[IsSpecialbeställning], v.[IsPrio], v.[StartadAvID], v.[Status], v.[FörväntadTillverkningsTid], v.[KundID]
                FROM (VALUES
                    (100000001, N'', CAST(1299.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2024-06-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 1, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1001),
                    (100000002, N'', CAST(1099.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2024-08-01T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 1, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1002),
                    (100000003, N'', CAST(299.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2024-06-21T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 1, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1003),
                    (100000004, N'', CAST(2399.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2024-06-21T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 1, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1004),
                    (100000005, N'', CAST(779.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2024-06-21T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 1, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1005),
                    (100000006, N'', CAST(949.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-02-18T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 2, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1001),
                    (100000007, N'', CAST(1049.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2025-10-06T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 2, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1002),
                    (100000008, N'', CAST(749.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 2, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1003),
                    (100000009, N'', CAST(999.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 2, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1004),
                    (100000010, N'', CAST(899.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 2, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1004),
                    (100000011, N'', CAST(1099.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 2, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1005),
                    (100000012, N'', CAST(2019.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 3, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1001),
                    (100000013, N'', CAST(1829.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 3, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1002),
                    (100000014, N'', CAST(599.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 3, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1003),
                    (100000015, N'', CAST(899.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 3, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1004),
                    (100000016, N'', CAST(1299.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(1 AS bit), CAST(0 AS bit), 3, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1005),
                    (100000017, N'', CAST(499.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 4, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1001),
                    (100000018, N'', CAST(499.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 4, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1002),
                    (100000019, N'', CAST(499.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 4, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1003),
                    (100000020, N'', CAST(499.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 4, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1004),
                    (100000021, N'', CAST(499.00 AS decimal(18,2)), CAST(NULL AS float), CAST('2026-04-11T00:00:00' AS datetime2), CAST(0 AS bit), CAST(0.00 AS decimal(18,2)), CAST(0 AS bit), CAST(0 AS bit), 4, N'Ej påbörjat', CAST('2026-04-28T00:00:00' AS datetime2), 1005)
                ) AS v ([OrderID], [Varukod], [Pris], [Moms], [Datum], [Färdig], [Rabatt], [IsSpecialbeställning], [IsPrio], [StartadAvID], [Status], [FörväntadTillverkningsTid], [KundID])
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM [Ordrar] o
                    WHERE o.[OrderID] = v.[OrderID]
                );

                SET IDENTITY_INSERT [Ordrar] OFF;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM [Ordrar]
                WHERE [OrderID] BETWEEN 100000001 AND 100000021;
                """);
        }
    }
}
