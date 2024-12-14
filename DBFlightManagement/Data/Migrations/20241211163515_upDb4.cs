using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBFlightManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class upDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrivalCity",
                schema: "Identity",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCity",
                schema: "Identity",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalCity",
                schema: "Identity",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "DepartmentCity",
                schema: "Identity",
                table: "Ticket");
        }
    }
}
