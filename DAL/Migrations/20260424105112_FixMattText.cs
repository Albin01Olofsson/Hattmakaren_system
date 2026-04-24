using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixMattText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
