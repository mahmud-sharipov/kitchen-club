using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenClube.Migrations.MySql
{
    public partial class RoleRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("bd5866dd-f2d8-45ed-84c5-15f266175d8e"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("c2d82c5c-5410-4fd7-9569-1e5e18527180"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("ed6c8fa8-4211-46c1-8b4b-831f2f50776b"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("44a383b7-77ff-4ca7-84ff-14ed9651839e"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Image", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("16000d1e-16a8-478b-8f80-0444699158ea"), "Бо гӯшти гӯспанд", "images/kazan.png", true, "Kazan-Kebab" },
                    { new Guid("30074704-1655-4e4b-bcbf-90bace8ff1c1"), "Бо гӯшти гов", "images/jazza.png", true, "Jazza" },
                    { new Guid("f6be27b2-0835-48b5-8d2b-5ed09a397a5b"), "1 ба 1", "images/osh.png", true, "Osh" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "EndDate", "StartDate", "Status" },
                values: new object[] { new Guid("c7cf993b-06c9-473a-92c7-a23f63b82beb"), new DateTime(2022, 9, 1, 15, 16, 21, 512, DateTimeKind.Local).AddTicks(2105), new DateTime(2022, 9, 1, 15, 16, 21, 512, DateTimeKind.Local).AddTicks(2096), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("16000d1e-16a8-478b-8f80-0444699158ea"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("30074704-1655-4e4b-bcbf-90bace8ff1c1"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("f6be27b2-0835-48b5-8d2b-5ed09a397a5b"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("c7cf993b-06c9-473a-92c7-a23f63b82beb"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Image", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("bd5866dd-f2d8-45ed-84c5-15f266175d8e"), "1 ба 1", "images/osh.png", true, "Osh" },
                    { new Guid("c2d82c5c-5410-4fd7-9569-1e5e18527180"), "Бо гӯшти гов", "images/jazza.png", true, "Jazza" },
                    { new Guid("ed6c8fa8-4211-46c1-8b4b-831f2f50776b"), "Бо гӯшти гӯспанд", "images/kazan.png", true, "Kazan-Kebab" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "EndDate", "StartDate", "Status" },
                values: new object[] { new Guid("44a383b7-77ff-4ca7-84ff-14ed9651839e"), new DateTime(2022, 9, 1, 15, 0, 32, 378, DateTimeKind.Local).AddTicks(440), new DateTime(2022, 9, 1, 15, 0, 32, 378, DateTimeKind.Local).AddTicks(430), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
