using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheets.Migrations
{
    public partial class DBInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "136d38fb-0fcd-4426-9939-183819c0aa5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e7ae09f-60ae-4552-8705-3b63a1fa5bda");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6b30f17-e1a5-4401-aa86-75b648390684");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "a6b30f17-e1a5-4401-aa86-75b648390684", "22e1e1ef-4587-4477-a7e8-dffe11b860fb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "136d38fb-0fcd-4426-9939-183819c0aa5b", "36fe4016-cee6-4da2-a0d1-ccbcdc879087", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e7ae09f-60ae-4552-8705-3b63a1fa5bda", "2496a158-139b-45e5-8018-bfa674b55ceb", "Manager", "MANAGER" });
        }
    }
}
