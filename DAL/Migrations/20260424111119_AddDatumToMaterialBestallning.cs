using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDatumToMaterialBestallning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "MaterialBeställningar",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$HJitNuMptB/jSQwTLNh9Muup2lcw7qseJrjTn8OWSi6X5HSkHP0yC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$HJitNuMptB/jSQwTLNh9Muup2lcw7qseJrjTn8OWSi6X5HSkHP0yC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$HJitNuMptB/jSQwTLNh9Muup2lcw7qseJrjTn8OWSi6X5HSkHP0yC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$HJitNuMptB/jSQwTLNh9Muup2lcw7qseJrjTn8OWSi6X5HSkHP0yC");

            migrationBuilder.UpdateData(
                table: "MaterialBeställningar",
                keyColumn: "MaterialBeställningID",
                keyValue: 1000001,
                column: "Datum",
                value: null);

            migrationBuilder.UpdateData(
                table: "MaterialBeställningar",
                keyColumn: "MaterialBeställningID",
                keyValue: 1000002,
                column: "Datum",
                value: null);

            migrationBuilder.UpdateData(
                table: "MaterialBeställningar",
                keyColumn: "MaterialBeställningID",
                keyValue: 1000003,
                column: "Datum",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datum",
                table: "MaterialBeställningar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$gQZAQbGiWomBbN0djERu0.R9klhIV4Rtmg.3QAq2X.xBoaTThLQ0K");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$gQZAQbGiWomBbN0djERu0.R9klhIV4Rtmg.3QAq2X.xBoaTThLQ0K");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$gQZAQbGiWomBbN0djERu0.R9klhIV4Rtmg.3QAq2X.xBoaTThLQ0K");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$gQZAQbGiWomBbN0djERu0.R9klhIV4Rtmg.3QAq2X.xBoaTThLQ0K");
        }
    }
}
