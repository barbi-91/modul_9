using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebshop.Migrations
{
    public partial class CreatedAdminAcountSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14fed3de-9e17-4fe6-8e27-8bf45190ad08",
                column: "ConcurrencyStamp",
                value: "7b474fd5-5b12-4b54-b62a-a9f8b07cadf8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e0aeba-595d-47d6-866f-83126dbc496f",
                column: "ConcurrencyStamp",
                value: "8da9dc41-4706-4d73-a25e-456a7ffe4c4e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b54b2adc7b39451e9796c022fae13794", 0, "Stara Cesta bb", "71366430-60d5-4462-9de2-0f9f8fabd80c", "mico@admin.com", false, "Mićo", "Programerić", false, null, "MICO@ADMIN.COM", "MICO@ADMIN.COM", "AQAAAAEAACcQAAAAEKbVazmoZ5D5hm051QLQ5N+vINVHfF3RVzhdB8qCXpWvYliRF8igYxCw9StwXJ6wUw==", null, false, "4a90d956-7a67-49fa-a5ad-f5f0517a7a96", false, "mico@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "60e0aeba-595d-47d6-866f-83126dbc496f", "b54b2adc7b39451e9796c022fae13794" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "60e0aeba-595d-47d6-866f-83126dbc496f", "b54b2adc7b39451e9796c022fae13794" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b54b2adc7b39451e9796c022fae13794");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14fed3de-9e17-4fe6-8e27-8bf45190ad08",
                column: "ConcurrencyStamp",
                value: "0ed6993c-14f6-45dc-a7be-d8f87ae4f3a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e0aeba-595d-47d6-866f-83126dbc496f",
                column: "ConcurrencyStamp",
                value: "1e07d7f9-e01f-40df-884a-7c9967100a02");
        }
    }
}
