using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDev.Data.Migrations
{
    public partial class AlterBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoreOwnerId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Books_StoreOwnerId",
                table: "Books",
                column: "StoreOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_StoreOwnerId",
                table: "Books",
                column: "StoreOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_StoreOwnerId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_StoreOwnerId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StoreOwnerId",
                table: "Books");
        }
    }
}
