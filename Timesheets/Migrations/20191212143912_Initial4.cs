using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class Initial4 : Migration
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
                values: new object[] { "e5392c53-12a9-4ef2-8e40-acd0b59e245c", "c33a4d66-5799-4654-a89a-f9e3f2ff8b6f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d891dec8-0e7b-4ddc-ac9c-becd216f1274", "4273f62f-3d44-469c-b024-fe136cd92726", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "50d5961b-f2a4-4a0d-86a5-c5128267bf3d", "47d4eacf-f113-4583-b485-305f052546cb", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50d5961b-f2a4-4a0d-86a5-c5128267bf3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d891dec8-0e7b-4ddc-ac9c-becd216f1274");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5392c53-12a9-4ef2-8e40-acd0b59e245c");

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
