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
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interests = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                values: new object[] { 1, "Greenbush", "Martinique", new DateTime(2020, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1956, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "Sit sit incididunt proident velit.", "Sunt esse aliqua ullamco in incididunt consequat commodo...", "Lisa", new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 184, 7, 238, 123, 58, 220, 189, 128, 27, 171, 204, 149, 81, 232, 235, 146, 122, 28, 4, 225, 244, 125, 240, 18, 13, 87, 213, 243, 217, 107, 140, 131, 127, 245, 80, 170, 125, 248, 216, 11, 64, 96, 163, 248, 146, 150, 44, 54, 115, 117, 202, 27, 103, 117, 154, 74, 222, 46, 109, 88, 164, 27, 52, 174 }, new byte[] { 1, 155, 157, 8, 43, 163, 75, 226, 33, 107, 212, 26, 79, 174, 31, 10, 108, 157, 175, 192, 37, 184, 2, 97, 188, 221, 155, 101, 157, 160, 38, 182, 20, 144, 206, 110, 209, 105, 247, 197, 171, 160, 233, 2, 50, 207, 40, 193, 53, 153, 207, 251, 252, 104, 109, 229, 15, 247, 79, 78, 117, 86, 23, 208, 101, 7, 64, 190, 18, 41, 177, 207, 159, 214, 88, 81, 108, 82, 135, 50, 174, 198, 92, 202, 102, 64, 191, 165, 76, 53, 125, 56, 180, 36, 189, 84, 74, 82, 237, 122, 220, 163, 156, 114, 195, 187, 139, 27, 144, 218, 175, 123, 66, 50, 123, 228, 189, 62, 132, 26, 181, 15, 78, 172, 205, 158, 16, 164 }, "Lisa" });

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
