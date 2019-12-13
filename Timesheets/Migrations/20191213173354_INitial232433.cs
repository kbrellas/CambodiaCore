using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class INitial232433 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62862a5b-5527-4962-9ffc-0c57d748d7db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a1cddcd-ffca-48a2-bb7a-ff06c1515b73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c739f9df-b348-4b0a-a228-9cbc923c8c91");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "397d9fea-65ba-4469-889e-e4d4049e7513", "38bdab69-add8-4c6a-8dd3-a2209495c7ef", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e028ec2-b41a-47b3-97e1-9e25d5d47849", "6902ec90-b0b5-45fe-bb1f-9bf93058dd82", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f35da00c-4cdf-42f1-9991-530916f49dde", "d71835c4-760b-4a6b-a723-70192023183a", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "397d9fea-65ba-4469-889e-e4d4049e7513");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e028ec2-b41a-47b3-97e1-9e25d5d47849");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f35da00c-4cdf-42f1-9991-530916f49dde");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "62862a5b-5527-4962-9ffc-0c57d748d7db", "d85f15d2-a964-401f-b632-15e4a5f7ac81", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c739f9df-b348-4b0a-a228-9cbc923c8c91", "12310ddd-41b3-4da0-b707-a6a6e5347e60", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7a1cddcd-ffca-48a2-bb7a-ff06c1515b73", "720d6205-4d5b-4000-be6c-74f060c60acf", "Manager", "MANAGER" });
        }
    }
}
