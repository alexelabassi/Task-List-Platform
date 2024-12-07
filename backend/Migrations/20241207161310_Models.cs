using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_List_Platform.Migrations
{
    /// <inheritdoc />
    public partial class Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78daff37-2856-4516-9fbf-a5a318270214");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e21e07c3-81a4-4eff-90c5-6517d6093723");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "851e3c56-aa9c-43f4-8c47-e7675cf902dd", null, "Admin", "ADMIN" },
                    { "dd5a0c79-a096-4bb7-978c-b1e826c42a74", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851e3c56-aa9c-43f4-8c47-e7675cf902dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd5a0c79-a096-4bb7-978c-b1e826c42a74");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78daff37-2856-4516-9fbf-a5a318270214", null, "User", "USER" },
                    { "e21e07c3-81a4-4eff-90c5-6517d6093723", null, "Admin", "ADMIN" }
                });
        }
    }
}
