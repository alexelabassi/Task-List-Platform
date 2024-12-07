using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_List_Platform.Migrations
{
    /// <inheritdoc />
    public partial class UserList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_AspNetUsers_UserId1",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_UserId1",
                table: "TaskLists");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ade2ff3-f2ee-47d0-bac7-02665b83d1ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1804ac1-eab9-43d4-9265-840a69670174");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaskLists");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TaskLists",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f05e6282-5e9f-4869-9aee-e2caf6dcedcd", null, "User", "USER" },
                    { "ffbf5f47-5f85-4c73-ab5c-ea1c0f36d38e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_UserId",
                table: "TaskLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_AspNetUsers_UserId",
                table: "TaskLists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_AspNetUsers_UserId",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_UserId",
                table: "TaskLists");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f05e6282-5e9f-4869-9aee-e2caf6dcedcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffbf5f47-5f85-4c73-ab5c-ea1c0f36d38e");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TaskLists",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TaskLists",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ade2ff3-f2ee-47d0-bac7-02665b83d1ee", null, "User", "USER" },
                    { "b1804ac1-eab9-43d4-9265-840a69670174", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_UserId1",
                table: "TaskLists",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_AspNetUsers_UserId1",
                table: "TaskLists",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
