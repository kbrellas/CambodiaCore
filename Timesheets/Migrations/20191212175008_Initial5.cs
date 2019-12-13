using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "610dc917-8264-4621-9565-a9d4d3883561", "ae066327-ba83-4712-af4c-b6734c09b19a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d866d15-1866-42d5-b53b-ffebe179d37e", "2ecbc32e-eaa7-4417-84b3-e7c8a0354bf4", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1ee9756-620b-4aef-a9a9-22f233984f74", "3b37c302-0405-47ed-a422-25c5242961af", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d866d15-1866-42d5-b53b-ffebe179d37e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "610dc917-8264-4621-9565-a9d4d3883561");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1ee9756-620b-4aef-a9a9-22f233984f74");

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
    }
}
