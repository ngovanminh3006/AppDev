using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDev.Data.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1fdb5aa-01b4-4914-96cf-3473e5d91ff4", "f69bdaa4-77f8-4700-88ac-6e7405ca97a2", "Customer", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1300c40-295c-4479-9b85-2a53ae4f93bd", "aa61d693-91e8-437a-bfbd-6ca29de30dd5", "StoreOwner", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7ac1b11-e41c-4e00-b8e5-5860b366598b", "141ff3ee-5e77-46e8-a0d2-d3413e551266", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1fdb5aa-01b4-4914-96cf-3473e5d91ff4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1300c40-295c-4479-9b85-2a53ae4f93bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7ac1b11-e41c-4e00-b8e5-5860b366598b");
        }
    }
}
