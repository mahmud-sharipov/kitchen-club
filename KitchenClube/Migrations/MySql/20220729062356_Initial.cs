using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenClube.Migrations.MySql
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Day = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FoodId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MenuId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserMenuItemSelections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Vote = table.Column<int>(type: "int", nullable: false),
                    MenuitemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Image", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("3f6a9572-91b4-4f92-81cd-a2037de19de4"), "Бо гӯшти гов", "images/jazza.png", true, "Jazza" },
                    { new Guid("a7ae556f-178c-4e07-a0b0-bd4f431c6dd9"), "Бо гӯшти гӯспанд", "images/kazan.png", true, "Kazan-Kebab" },
                    { new Guid("f12f0134-8043-4cb8-9590-da79c738cf52"), "1 ба 1", "images/osh.png", true, "Osh" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "EndDate", "StartDate", "Status" },
                values: new object[] { new Guid("fbb70259-8cb1-4b3d-9da6-4a0b0586d45b"), new DateTime(2022, 7, 29, 11, 23, 56, 118, DateTimeKind.Local).AddTicks(623), new DateTime(2022, 7, 29, 11, 23, 56, 118, DateTimeKind.Local).AddTicks(606), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("01869113-04bb-4a83-ab3c-e5d9dba45d3c"), "azizjon@gmail.com", "Azizjon", true, "", "+992 92 929 2992" },
                    { new Guid("2120d68e-8b8e-4fad-9348-ca0afef4c5af"), "karimjon@gamil.com", "Karimjon", true, "", "+992 92 888 77 66" },
                    { new Guid("5b5de6cc-a6fb-4433-ac37-8a815a81a595"), "amirjon@gmail.com", "Amirjon", true, "", "+992 92 777 00 77" }
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
