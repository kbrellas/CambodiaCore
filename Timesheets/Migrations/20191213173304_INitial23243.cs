using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class INitial23243 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "287f3172-e608-4e6c-8bab-bb59bba12f23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fe506a6-d532-48c6-a5b0-e3f70d169b24");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "680b28f4-3168-4198-9216-d19ce9f581c7");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "2fe506a6-d532-48c6-a5b0-e3f70d169b24", "71bfddfd-a770-4a72-82ff-3e0d2e4fc092", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "287f3172-e608-4e6c-8bab-bb59bba12f23", "40866000-9e10-45cb-9205-3c262b8579f2", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "680b28f4-3168-4198-9216-d19ce9f581c7", "652d6651-62ea-4467-a244-d61da8f4bb1d", "Manager", "MANAGER" });
        }
    }
}
