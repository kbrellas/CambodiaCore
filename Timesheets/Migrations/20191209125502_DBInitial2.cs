using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class DBInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82985b45-2d5a-443b-8d2d-f030a5ba60ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc5f8e87-dd1c-468f-a3eb-5eac34b590de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4b0a429-4b0f-4fc0-aef6-d7ee14353d7d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ff0d9a9-d946-425d-b9a4-f0b459dc2a04", "6939ee2b-5ea1-41d1-bf98-ff7608f2fae2", "Admin", "ADMIN" },
                    { "506140a4-13ef-4d12-b02f-3f8ea493e9db", "645eff8d-43a1-4ff0-a6b9-5cf4c52ce7ec", "Employee", "EMPLOYEE" },
                    { "ef9c25bd-f77f-4c02-b2bf-9bc192288399", "f203dae8-7ce7-47c0-9374-aadf4853505d", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentHeadId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Department 1" },
                    { 2, null, "Department 2" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name", "OwnerDeptId" },
                values: new object[,]
                {
                    { 1, "Project1", null },
                    { 2, "Project 2", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82985b45-2d5a-443b-8d2d-f030a5ba60ae", "29b9e030-051d-4ecf-8f4b-9b1c9ac06e87", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4b0a429-4b0f-4fc0-aef6-d7ee14353d7d", "3181a003-6a99-4608-a492-1606542337a9", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc5f8e87-dd1c-468f-a3eb-5eac34b590de", "e9728da5-e334-4b09-af81-58baa9df89d1", "Manager", "MANAGER" });
        }
    }
}
