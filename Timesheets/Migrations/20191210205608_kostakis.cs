using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class kostakis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ff0d9a9-d946-425d-b9a4-f0b459dc2a04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "506140a4-13ef-4d12-b02f-3f8ea493e9db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef9c25bd-f77f-4c02-b2bf-9bc192288399");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "88dd203d-30fe-4fd0-94ac-afd9d142b228", "cbaa7513-c87f-4758-b706-ce2a072ded5d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01ab8411-6fbe-492f-93e4-b03e1a7e0a92", "b4a39f2d-56d0-4372-88fb-edb14b7aea57", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c9ef7cb-1014-4bef-a5ef-1a7ddef905e5", "3e772d57-b767-41a9-8ff0-cdcc359c0a00", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01ab8411-6fbe-492f-93e4-b03e1a7e0a92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c9ef7cb-1014-4bef-a5ef-1a7ddef905e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88dd203d-30fe-4fd0-94ac-afd9d142b228");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4ff0d9a9-d946-425d-b9a4-f0b459dc2a04", "6939ee2b-5ea1-41d1-bf98-ff7608f2fae2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "506140a4-13ef-4d12-b02f-3f8ea493e9db", "645eff8d-43a1-4ff0-a6b9-5cf4c52ce7ec", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef9c25bd-f77f-4c02-b2bf-9bc192288399", "f203dae8-7ce7-47c0-9374-aadf4853505d", "Manager", "MANAGER" });
        }
    }
}
