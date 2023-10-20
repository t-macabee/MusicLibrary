using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KnownAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Interests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Country", "Created", "DateOfBirth", "Interests", "KnownAs", "LastActive", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { 1, "Martinique", new DateTime(2020, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1956, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sit sit incididunt proident velit.", "Lisa", new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 170, 135, 254, 6, 231, 151, 92, 156, 249, 69, 12, 11, 55, 153, 225, 245, 49, 185, 38, 142, 16, 92, 17, 68, 211, 242, 18, 224, 198, 76, 117, 205, 230, 3, 150, 4, 239, 169, 49, 170, 235, 125, 146, 116, 56, 214, 108, 245, 79, 68, 234, 139, 29, 30, 32, 240, 145, 82, 144, 224, 42, 153, 224, 4 }, new byte[] { 201, 231, 132, 167, 137, 153, 232, 159, 205, 242, 146, 232, 77, 174, 166, 123, 86, 57, 147, 253, 218, 247, 129, 1, 45, 231, 148, 154, 192, 211, 37, 52, 149, 77, 220, 43, 134, 116, 64, 199, 215, 78, 226, 121, 87, 19, 75, 163, 163, 209, 86, 55, 224, 195, 108, 222, 190, 73, 115, 71, 91, 94, 18, 87, 87, 193, 36, 239, 241, 195, 32, 156, 237, 56, 114, 238, 184, 160, 13, 94, 129, 130, 181, 38, 61, 172, 140, 17, 96, 230, 210, 178, 70, 180, 13, 233, 120, 47, 216, 247, 82, 174, 26, 27, 37, 52, 124, 126, 195, 114, 69, 26, 73, 187, 62, 150, 46, 62, 208, 248, 175, 231, 114, 147, 173, 176, 174, 66 }, "Lisa" });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "AppUserId", "IsMain", "PublicId", "Url" },
                values: new object[] { 1, 1, true, "PublicId1", "https://randomuser.me/api/portraits/women/54.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
