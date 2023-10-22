using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    SenderUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    RecipientUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MessageSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecipientDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 27, 89, 244, 223, 99, 4, 135, 100, 94, 195, 71, 151, 143, 236, 86, 140, 39, 245, 12, 88, 140, 228, 238, 222, 76, 158, 79, 9, 65, 59, 45, 93, 30, 82, 179, 254, 190, 228, 243, 144, 192, 131, 4, 151, 0, 248, 169, 128, 5, 155, 213, 145, 170, 106, 132, 173, 245, 57, 229, 199, 74, 50, 249, 187 }, new byte[] { 225, 157, 2, 224, 134, 140, 95, 55, 24, 10, 46, 74, 113, 208, 89, 213, 102, 113, 18, 48, 92, 106, 62, 28, 154, 167, 87, 105, 246, 150, 155, 6, 229, 17, 142, 224, 227, 12, 4, 182, 210, 243, 128, 120, 7, 198, 211, 127, 202, 31, 103, 72, 161, 126, 37, 167, 56, 159, 98, 188, 66, 180, 177, 106, 84, 226, 183, 232, 153, 239, 59, 215, 202, 120, 5, 186, 111, 140, 28, 31, 43, 41, 172, 223, 174, 232, 254, 110, 154, 4, 200, 153, 192, 208, 57, 253, 190, 130, 249, 99, 238, 222, 65, 1, 191, 154, 23, 66, 172, 243, 179, 182, 198, 236, 207, 127, 236, 108, 79, 154, 252, 131, 49, 120, 177, 175, 166, 67 } });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 170, 135, 254, 6, 231, 151, 92, 156, 249, 69, 12, 11, 55, 153, 225, 245, 49, 185, 38, 142, 16, 92, 17, 68, 211, 242, 18, 224, 198, 76, 117, 205, 230, 3, 150, 4, 239, 169, 49, 170, 235, 125, 146, 116, 56, 214, 108, 245, 79, 68, 234, 139, 29, 30, 32, 240, 145, 82, 144, 224, 42, 153, 224, 4 }, new byte[] { 201, 231, 132, 167, 137, 153, 232, 159, 205, 242, 146, 232, 77, 174, 166, 123, 86, 57, 147, 253, 218, 247, 129, 1, 45, 231, 148, 154, 192, 211, 37, 52, 149, 77, 220, 43, 134, 116, 64, 199, 215, 78, 226, 121, 87, 19, 75, 163, 163, 209, 86, 55, 224, 195, 108, 222, 190, 73, 115, 71, 91, 94, 18, 87, 87, 193, 36, 239, 241, 195, 32, 156, 237, 56, 114, 238, 184, 160, 13, 94, 129, 130, 181, 38, 61, 172, 140, 17, 96, 230, 210, 178, 70, 180, 13, 233, 120, 47, 216, 247, 82, 174, 26, 27, 37, 52, 124, 126, 195, 114, 69, 26, 73, 187, 62, 150, 46, 62, 208, 248, 175, 231, 114, 147, 173, 176, 174, 66 } });
        }
    }
}
