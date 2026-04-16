using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class isprioOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrio",
                table: "Ordrar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$zrNjwijy7Io2SltLrp/xr.Aaj01F3LtfKGmLC..cgLSn9qY.OgGcq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$zrNjwijy7Io2SltLrp/xr.Aaj01F3LtfKGmLC..cgLSn9qY.OgGcq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$zrNjwijy7Io2SltLrp/xr.Aaj01F3LtfKGmLC..cgLSn9qY.OgGcq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$zrNjwijy7Io2SltLrp/xr.Aaj01F3LtfKGmLC..cgLSn9qY.OgGcq");

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000001,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000002,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000003,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000004,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000005,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000006,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000007,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000008,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000009,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000010,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000011,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000012,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000013,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000014,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000015,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000016,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000017,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000018,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000019,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000020,
                column: "IsPrio",
                value: false);

            migrationBuilder.UpdateData(
                table: "Ordrar",
                keyColumn: "OrderID",
                keyValue: 100000021,
                column: "IsPrio",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrio",
                table: "Ordrar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$M4Y5bFoUaA4xkxmhUdq6peE3YyrfA/cjrMts7QsZMTREgIpCuzIYm");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$M4Y5bFoUaA4xkxmhUdq6peE3YyrfA/cjrMts7QsZMTREgIpCuzIYm");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$M4Y5bFoUaA4xkxmhUdq6peE3YyrfA/cjrMts7QsZMTREgIpCuzIYm");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$M4Y5bFoUaA4xkxmhUdq6peE3YyrfA/cjrMts7QsZMTREgIpCuzIYm");
        }
    }
}
