using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebshop.Migrations
{
    public partial class FixOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14fed3de-9e17-4fe6-8e27-8bf45190ad08",
                column: "ConcurrencyStamp",
                value: "e44d958d-e9a9-4a28-9ae3-1a9f7c4ad5e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e0aeba-595d-47d6-866f-83126dbc496f",
                column: "ConcurrencyStamp",
                value: "18aeec64-80d7-4c30-b6b9-9ef6f1c4e2b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b54b2adc7b39451e9796c022fae13794",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a7fb98f-c15d-4b0b-be7a-e090a7396b72", "AQAAAAEAACcQAAAAEI+8U4a0xfAXkTaQWuGxnZGQZGv0QzSeAyvkhtDYU8b/KkFIBmRtt7vkgW0Ppvhnew==", "d018ac5b-6ae5-47f0-8338-984b264216ee" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14fed3de-9e17-4fe6-8e27-8bf45190ad08",
                column: "ConcurrencyStamp",
                value: "e395b4b5-4ba4-4aac-ab30-0ceec777d3ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60e0aeba-595d-47d6-866f-83126dbc496f",
                column: "ConcurrencyStamp",
                value: "4e2fa2e3-bd7f-4969-9671-6020580b9569");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b54b2adc7b39451e9796c022fae13794",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab7a59c2-985e-47f9-a057-ce31b74bf3bd", "AQAAAAEAACcQAAAAEKZqSwKql8FYDigwNm3LfJT8+iyT4wkLFfDJPP5CdUiokFqb52Pi58WIAEma/w8Cig==", "6ccf4877-599e-4eca-81d6-17f2b6f4f642" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
