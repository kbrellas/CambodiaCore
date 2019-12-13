using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class INitial2324 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00a26872-4a04-44e7-b29d-95f4e82cf481");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a738dc2c-55c0-4307-84bb-f4ff28e9022e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbf03b70-8e03-4de4-8812-213f0f3d4e67");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "fbf03b70-8e03-4de4-8812-213f0f3d4e67", "4613d188-6f8b-4fe2-977f-d6bf412e6f3d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a738dc2c-55c0-4307-84bb-f4ff28e9022e", "13b55210-bc89-4357-8395-0afc590274ca", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00a26872-4a04-44e7-b29d-95f4e82cf481", "74071c16-f38c-4951-baf4-91211c2e0511", "Manager", "MANAGER" });
        }
    }
}
