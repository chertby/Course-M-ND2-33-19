using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Htp.ITnews.Data.EntityFramework.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("0931544e-4fd6-4d76-aff1-4cef34903e8a"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("33459892-64fa-4a15-a8f7-07275c3077df"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("4cf5fdfa-8a41-4e9e-9466-8c06bd42d3fc"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("8205e48a-8671-48cb-9be6-2cc2ff7c7250"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("967b30a3-9852-45cb-b33e-efa6876f04d4"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("82756cea-53a4-4be6-90f8-776c028bd05f"), "e270c5c2-bdaf-438a-833c-a19336d9c64a", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("984d76af-82dc-4f56-9a8a-034dcb5baa35"), "e49388fc-93ec-4c8c-845a-4726c0663aa1", "Writer", "WRITER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8a707444-f904-4ba6-88e8-0a9b7b2dca36"), "dbdc357d-c958-4a9f-8148-d1df47f6d1b3", "Reader", "Reader" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("6cddc33a-15e6-4b13-b77e-b233b10873cc"), "Java" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("4256ce51-1d63-4c87-92cb-606cb8617107"), "C#" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("5ee9a6b0-c820-440c-b4ba-864f6b983ccf"), "C++" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("edfef407-8855-49f5-b80e-495fedad1d16"), "Algorithms" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("8c4a6d65-f60b-49e7-b18f-e24f4652d488"), "Machine Learning" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("82756cea-53a4-4be6-90f8-776c028bd05f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8a707444-f904-4ba6-88e8-0a9b7b2dca36"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("984d76af-82dc-4f56-9a8a-034dcb5baa35"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("4256ce51-1d63-4c87-92cb-606cb8617107"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("5ee9a6b0-c820-440c-b4ba-864f6b983ccf"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("6cddc33a-15e6-4b13-b77e-b233b10873cc"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("8c4a6d65-f60b-49e7-b18f-e24f4652d488"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("edfef407-8855-49f5-b80e-495fedad1d16"));

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("4cf5fdfa-8a41-4e9e-9466-8c06bd42d3fc"), "Java" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("8205e48a-8671-48cb-9be6-2cc2ff7c7250"), "C#" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("33459892-64fa-4a15-a8f7-07275c3077df"), "C++" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("0931544e-4fd6-4d76-aff1-4cef34903e8a"), "Algorithms" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("967b30a3-9852-45cb-b33e-efa6876f04d4"), "Machine Learning" });
        }
    }
}
