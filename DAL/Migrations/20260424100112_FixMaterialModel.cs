using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixMaterialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mått",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Typ",
                table: "Material");

            migrationBuilder.AddColumn<int>(
                name: "MåttTyp",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$MGXaTYjArDBnqETXHfuPoe5MWZ6wsi9F3.J9So01VHik/kdRzQYrK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$MGXaTYjArDBnqETXHfuPoe5MWZ6wsi9F3.J9So01VHik/kdRzQYrK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$MGXaTYjArDBnqETXHfuPoe5MWZ6wsi9F3.J9So01VHik/kdRzQYrK");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$MGXaTYjArDBnqETXHfuPoe5MWZ6wsi9F3.J9So01VHik/kdRzQYrK");

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialID",
                keyValue: 100001,
                column: "MåttTyp",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialID",
                keyValue: 100002,
                column: "MåttTyp",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialID",
                keyValue: 100003,
                column: "MåttTyp",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MåttTyp",
                table: "Material");

            migrationBuilder.AddColumn<string>(
                name: "Mått",
                table: "Material",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Typ",
                table: "Material",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 1,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 2,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 3,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Användare",
                keyColumn: "AnvändarID",
                keyValue: 4,
                column: "Lösenord",
                value: "$2a$11$nO1O37wAkAFVhfmFadMYsuSr5psQT8v4X6uro1EYeiI17AZt1bSxO");

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialID",
                keyValue: 100001,
                columns: new[] { "Mått", "Typ" },
                values: new object[] { "meter", "Tyg" });

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialID",
                keyValue: 100002,
                columns: new[] { "Mått", "Typ" },
                values: new object[] { "milimeter", "Tyg" });

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialID",
                keyValue: 100003,
                columns: new[] { "Mått", "Typ" },
                values: new object[] { "meter", "Tråd" });
        }
    }
}
