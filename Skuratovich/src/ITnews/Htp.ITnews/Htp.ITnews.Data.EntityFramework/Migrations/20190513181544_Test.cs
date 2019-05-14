using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Htp.ITnews.Data.EntityFramework.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f0ffe9f-c359-40d7-8f4f-0502f2ff33ba"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c0f27d03-e207-4cbd-a181-c8bbf69b6165"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d5871ab0-8cab-4419-b200-843eeb090290"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7db4f65c-ff90-492f-ae41-8781c95ce890"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b5423bf7-3ee9-40c4-b9d7-45433081f70c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f0e59075-aa98-4ee7-abd8-e32ff862ca45"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("05c9a13f-946a-4ec0-9d8b-95f3a18ad961"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("0fe2e7bd-556f-4fcf-a9c0-9ddbdd761b19"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("3b93c40a-07e1-4988-a563-70bce485086d"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("5090278b-c414-4856-8da2-3c845e68a1df"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("612efff5-3883-496f-ad5d-6dbd500f5efa"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5b767b9c-6128-4ac0-9f50-8fe1b0941fbf"), "40bfd024-e9c4-436d-b182-5278da8ff6b1", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("938cbe76-89b7-452c-ba78-baccbeb1cff2"), "24ef1ebd-33c1-4f82-9fa4-9fb868a1b160", "Writer", "WRITER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2d29f509-eac4-4e0c-b47f-248a1ab297b0"), "53e48d0d-02aa-49ae-82e1-0d9db9e66e66", "Reader", "READER" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("7679e8c1-d5ea-45aa-ab74-ec5801b1e002"), "C" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("8b8e3f9e-7849-4110-bf1c-2cf7762e1726"), "C++" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("213e3db0-5557-4790-9670-85042d92188b"), "C#" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("a6b35e25-570d-413c-9b8f-d977a9548f53"), "Java" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("a6483c80-3589-4613-a9c7-c7f4479aab50"), "C#" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("b7fdbdc9-e4f2-4b76-8953-f603f3c9ef23"), "C++" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("eb4e6423-b3c9-4fdc-bb5a-4be32679d48d"), "Algorithms" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("f1b96dbd-183c-4efb-a95d-3ff2bfe7c2f6"), "Machine Learning" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2d29f509-eac4-4e0c-b47f-248a1ab297b0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5b767b9c-6128-4ac0-9f50-8fe1b0941fbf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("938cbe76-89b7-452c-ba78-baccbeb1cff2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("213e3db0-5557-4790-9670-85042d92188b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("7679e8c1-d5ea-45aa-ab74-ec5801b1e002"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("8b8e3f9e-7849-4110-bf1c-2cf7762e1726"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("a6483c80-3589-4613-a9c7-c7f4479aab50"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("a6b35e25-570d-413c-9b8f-d977a9548f53"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("b7fdbdc9-e4f2-4b76-8953-f603f3c9ef23"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("eb4e6423-b3c9-4fdc-bb5a-4be32679d48d"));

            migrationBuilder.DeleteData(
                table: "Сategories",
                keyColumn: "Id",
                keyValue: new Guid("f1b96dbd-183c-4efb-a95d-3ff2bfe7c2f6"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d5871ab0-8cab-4419-b200-843eeb090290"), "c36b6d3d-1784-45c4-bcc7-cff7b92522af", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("c0f27d03-e207-4cbd-a181-c8bbf69b6165"), "8966083c-0945-475d-9516-201900af7631", "Writer", "WRITER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("6f0ffe9f-c359-40d7-8f4f-0502f2ff33ba"), "24d1e940-dcdd-4882-8517-bd5245fd1d77", "Reader", "READER" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("7db4f65c-ff90-492f-ae41-8781c95ce890"), "C" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("f0e59075-aa98-4ee7-abd8-e32ff862ca45"), "C++" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("b5423bf7-3ee9-40c4-b9d7-45433081f70c"), "C#" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("05c9a13f-946a-4ec0-9d8b-95f3a18ad961"), "Java" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("5090278b-c414-4856-8da2-3c845e68a1df"), "C#" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("3b93c40a-07e1-4988-a563-70bce485086d"), "C++" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("612efff5-3883-496f-ad5d-6dbd500f5efa"), "Algorithms" });

            migrationBuilder.InsertData(
                table: "Сategories",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("0fe2e7bd-556f-4fcf-a9c0-9ddbdd761b19"), "Machine Learning" });
        }
    }
}
