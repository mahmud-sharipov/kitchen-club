using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenClube.Migrations.SqlServer
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuItemSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    MenuitemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuItemSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuItemSelections_MenuItems_MenuitemId",
                        column: x => x.MenuitemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuItemSelections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Image", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("b3192fdd-c484-4558-a92c-85277076c389"), "1 ба 1", "images/osh.png", true, "Osh" },
                    { new Guid("b82375b0-761d-4073-a019-9d8f737efdf4"), "Бо гӯшти гов", "images/jazza.png", true, "Jazza" },
                    { new Guid("df7e0db4-4e07-405b-816f-9c1aee84ebd5"), "Бо гӯшти гӯспанд", "images/kazan.png", true, "Kazan-Kebab" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "EndDate", "StartDate", "Status" },
                values: new object[] { new Guid("e3e494c4-f4c8-41e1-a93e-e8eab21e5674"), new DateTime(2022, 7, 29, 9, 9, 20, 769, DateTimeKind.Local).AddTicks(1425), new DateTime(2022, 7, 29, 9, 9, 20, 769, DateTimeKind.Local).AddTicks(1418), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("5642ea1a-d5bb-4eda-8efe-89e1e794a788"), "karimjon@gamil.com", "Karimjon", true, "", "+992 92 888 77 66" },
                    { new Guid("8571eca7-d8d3-4e78-a43d-f48eadc8ba4e"), "azizjon@gmail.com", "Azizjon", true, "", "+992 92 929 2992" },
                    { new Guid("8a9c9de8-1da2-4ac7-a8ec-ac5715c57950"), "amirjon@gmail.com", "Amirjon", true, "", "+992 92 777 00 77" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_FoodId",
                table: "MenuItems",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuItemSelections_MenuitemId",
                table: "UserMenuItemSelections",
                column: "MenuitemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuItemSelections_UserId",
                table: "UserMenuItemSelections",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMenuItemSelections");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
