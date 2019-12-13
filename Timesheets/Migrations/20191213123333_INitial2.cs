using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class INitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2af28c60-4b59-4617-abe1-371d6e226e60");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f95c361-c601-439e-8df3-5cc027a4f6f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34b87de3-a0c3-41c9-9343-2de42c911177");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "515170ab-385f-4a1d-a9a1-24d5aca86b28", "8360b50a-95a2-4dab-be2f-3aceac7d837e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d774412a-f628-4fe1-9600-b4cee6d99fa2", "bd75aea8-e677-429e-a007-99696c5fb260", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14a32d7-ecc0-4585-8fc0-0e5ab645f114", "4bd9c228-fa99-407b-a1f6-10d9824f88b8", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "515170ab-385f-4a1d-a9a1-24d5aca86b28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14a32d7-ecc0-4585-8fc0-0e5ab645f114");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d774412a-f628-4fe1-9600-b4cee6d99fa2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34b87de3-a0c3-41c9-9343-2de42c911177", "05637768-5a38-46dc-bd73-d0e9ff677a81", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2af28c60-4b59-4617-abe1-371d6e226e60", "4886fcc1-45a5-49ea-9a4c-b18dfdf14b20", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f95c361-c601-439e-8df3-5cc027a4f6f4", "d11fd6b8-973a-4013-8886-43e4f6481edc", "Manager", "MANAGER" });
        }
    }
}
