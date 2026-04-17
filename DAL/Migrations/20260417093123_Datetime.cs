using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SlutTid",
                table: "Planeringar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTid",
                table: "Planeringar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$JUzSPnCSAh1.8Br0nunYeeIhC8HFd2w4kxxA43Et4Tf8627nx2uCq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlutTid",
                table: "Planeringar");

            migrationBuilder.DropColumn(
                name: "StartTid",
                table: "Planeringar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$7dyaVVvUceUYS8sW9mbLvelFU38ldY9640omTDL89caHprCLcUXFq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$7dyaVVvUceUYS8sW9mbLvelFU38ldY9640omTDL89caHprCLcUXFq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$7dyaVVvUceUYS8sW9mbLvelFU38ldY9640omTDL89caHprCLcUXFq");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$7dyaVVvUceUYS8sW9mbLvelFU38ldY9640omTDL89caHprCLcUXFq");
        }
    }
}
