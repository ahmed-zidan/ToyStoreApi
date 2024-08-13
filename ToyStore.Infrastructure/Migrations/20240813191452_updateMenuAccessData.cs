using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToyStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateMenuAccessData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuAccesses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuAccesses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuAccesses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuAccesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuAccesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuAccesses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "MenuAccesses");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "MenuAccesses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MenuAccesses_RoleId",
                table: "MenuAccesses",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAccesses_AspNetRoles_RoleId",
                table: "MenuAccesses",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuAccesses_AspNetRoles_RoleId",
                table: "MenuAccesses");

            migrationBuilder.DropIndex(
                name: "IX_MenuAccesses_RoleId",
                table: "MenuAccesses");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "MenuAccesses");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "MenuAccesses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }
    }
}
