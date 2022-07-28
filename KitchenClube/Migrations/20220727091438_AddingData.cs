using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenClube.Migrations
{
    public partial class AddingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Image", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("cf0a6ada-b316-4ed2-ba5b-047539513520"), "1 ба 1", "images/osh.png", true, "Osh" },
                    { new Guid("d004d696-3910-4721-8227-837a2e1db157"), "Бо гӯшти гӯспанд", "images/kazan.png", true, "Kazan-Kebab" },
                    { new Guid("f2aa1ecf-b080-4218-be79-f9beff2f08fe"), "Бо гӯшти гов", "images/jazza.png", true, "Jazza" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "EndDate", "StartDate", "Status" },
                values: new object[] { new Guid("ce08a354-da82-4cfb-9c5e-f36fa6eb8a39"), new DateTime(2022, 7, 27, 14, 14, 38, 324, DateTimeKind.Local).AddTicks(5869), new DateTime(2022, 7, 27, 14, 14, 38, 324, DateTimeKind.Local).AddTicks(5854), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("2d01ce09-510c-4d2e-a2b7-05fb28bb0963"), "azizjon@gmail.com", "Azizjon", true, "", "+992 92 929 2992" },
                    { new Guid("3bba2bd9-c6a8-47a1-abba-13d251d5f2d9"), "amirjon@gmail.com", "Amirjon", true, "", "+992 92 777 00 77" },
                    { new Guid("b7f7d40a-bf83-4738-adaf-4bbba281b68e"), "karimjon@gamil.com", "Karimjon", true, "", "+992 92 888 77 66" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("cf0a6ada-b316-4ed2-ba5b-047539513520"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("d004d696-3910-4721-8227-837a2e1db157"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("f2aa1ecf-b080-4218-be79-f9beff2f08fe"));

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("ce08a354-da82-4cfb-9c5e-f36fa6eb8a39"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2d01ce09-510c-4d2e-a2b7-05fb28bb0963"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3bba2bd9-c6a8-47a1-abba-13d251d5f2d9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b7f7d40a-bf83-4738-adaf-4bbba281b68e"));
        }
    }
}
