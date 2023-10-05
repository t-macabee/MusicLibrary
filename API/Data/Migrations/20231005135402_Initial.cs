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
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                columns: new[] { "Id", "City", "Country", "Created", "DateOfBirth", "Gender", "Interests", "Introduction", "KnownAs", "LastActive", "PasswordHash", "PasswordSalt", "UserName" },
                values: new object[] { 1, "Greenbush", "Martinique", new DateTime(2020, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1956, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "Sit sit incididunt proident velit.", "Sunt esse aliqua ullamco in incididunt consequat commodo...", "Lisa", new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 174, 243, 36, 86, 113, 34, 199, 130, 223, 251, 132, 205, 16, 218, 173, 22, 192, 8, 8, 117, 78, 47, 41, 121, 167, 229, 72, 18, 49, 235, 11, 110, 237, 165, 230, 50, 9, 248, 141, 119, 179, 78, 167, 150, 75, 148, 200, 13, 172, 233, 206, 67, 9, 100, 52, 20, 23, 168, 92, 182, 39, 223, 82, 78 }, new byte[] { 156, 88, 34, 45, 26, 49, 20, 8, 148, 78, 223, 91, 221, 93, 83, 92, 231, 176, 74, 103, 7, 194, 206, 152, 149, 137, 46, 211, 162, 251, 213, 184, 36, 6, 26, 35, 33, 14, 94, 208, 72, 189, 111, 91, 225, 58, 102, 211, 118, 246, 24, 241, 32, 248, 58, 46, 98, 140, 84, 77, 155, 105, 223, 167, 78, 17, 217, 221, 73, 11, 251, 90, 124, 96, 94, 101, 204, 31, 55, 112, 92, 14, 159, 63, 209, 8, 240, 97, 71, 251, 26, 183, 93, 145, 25, 172, 177, 66, 163, 37, 158, 119, 165, 218, 205, 90, 63, 237, 217, 174, 42, 204, 3, 13, 91, 9, 100, 217, 7, 79, 9, 52, 133, 153, 75, 230, 72, 247 }, "Lisa" });

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
