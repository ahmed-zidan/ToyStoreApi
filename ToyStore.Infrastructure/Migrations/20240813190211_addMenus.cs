using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToyStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    HaveView = table.Column<bool>(type: "bit", nullable: false),
                    HaveAdd = table.Column<bool>(type: "bit", nullable: false),
                    HaveEdit = table.Column<bool>(type: "bit", nullable: false),
                    HaveDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuAccesses_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Home", true },
                    { 2, "Product", true },
                    { 3, "Category", true },
                    { 4, "User Manager", true },
                    { 5, "Role Manager", true },
                    { 6, "Menu Manager", true }
                });

            migrationBuilder.InsertData(
                table: "MenuAccesses",
                columns: new[] { "Id", "HaveAdd", "HaveDelete", "HaveEdit", "HaveView", "MenuId", "Role" },
                values: new object[,]
                {
                    { 1, true, true, true, true, 1, "Admin" },
                    { 2, true, true, true, true, 2, "Admin" },
                    { 3, true, true, true, true, 3, "Admin" },
                    { 4, true, true, true, true, 4, "Admin" },
                    { 5, true, true, true, true, 5, "Admin" },
                    { 6, true, true, true, true, 6, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuAccesses_MenuId",
                table: "MenuAccesses",
                column: "MenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuAccesses");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
