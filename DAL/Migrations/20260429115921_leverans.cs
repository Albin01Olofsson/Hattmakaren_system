using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class leverans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Levererad",
                table: "MaterialBeställningar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$rKw9iz8r6.qCXeX5iix35ub48iisIgtSGj2qXza.Hre21nYZMBiSC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$rKw9iz8r6.qCXeX5iix35ub48iisIgtSGj2qXza.Hre21nYZMBiSC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$rKw9iz8r6.qCXeX5iix35ub48iisIgtSGj2qXza.Hre21nYZMBiSC");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$rKw9iz8r6.qCXeX5iix35ub48iisIgtSGj2qXza.Hre21nYZMBiSC");

            migrationBuilder.UpdateData(
                table: "MaterialBeställningar",
                keyColumn: "MaterialBeställningID",
                keyValue: 1000001,
                column: "Levererad",
                value: false);

            migrationBuilder.UpdateData(
                table: "MaterialBeställningar",
                keyColumn: "MaterialBeställningID",
                keyValue: 1000002,
                column: "Levererad",
                value: false);

            migrationBuilder.UpdateData(
                table: "MaterialBeställningar",
                keyColumn: "MaterialBeställningID",
                keyValue: 1000003,
                column: "Levererad",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Levererad",
                table: "MaterialBeställningar");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$6sWUJlyHcvFCZBYVMokc/eWeqCFmKUPZ0XD9Up.R5vTWBH5O55rHe");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$6sWUJlyHcvFCZBYVMokc/eWeqCFmKUPZ0XD9Up.R5vTWBH5O55rHe");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$6sWUJlyHcvFCZBYVMokc/eWeqCFmKUPZ0XD9Up.R5vTWBH5O55rHe");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$6sWUJlyHcvFCZBYVMokc/eWeqCFmKUPZ0XD9Up.R5vTWBH5O55rHe");
        }
    }
}
