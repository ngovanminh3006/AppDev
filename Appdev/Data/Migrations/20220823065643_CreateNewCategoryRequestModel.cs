using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDev.Data.Migrations
{
    public partial class CreateNewCategoryRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewCategoryRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StoreOwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsApproval = table.Column<bool>(type: "bit", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCategoryRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewCategoryRequests_AspNetUsers_StoreOwnerId",
                        column: x => x.StoreOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewCategoryRequests_Name",
                table: "NewCategoryRequests",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewCategoryRequests_StoreOwnerId",
                table: "NewCategoryRequests",
                column: "StoreOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewCategoryRequests");
        }
    }
}
