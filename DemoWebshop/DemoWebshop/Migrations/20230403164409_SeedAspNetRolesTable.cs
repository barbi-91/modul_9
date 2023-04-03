using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebshop.Migrations
{
    public partial class SeedAspNetRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14fed3de-9e17-4fe6-8e27-8bf45190ad08", "0ed6993c-14f6-45dc-a7be-d8f87ae4f3a5", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60e0aeba-595d-47d6-866f-83126dbc496f", "1e07d7f9-e01f-40df-884a-7c9967100a02", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14fed3de-9e17-4fe6-8e27-8bf45190ad08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e0aeba-595d-47d6-866f-83126dbc496f");
        }
    }
}
