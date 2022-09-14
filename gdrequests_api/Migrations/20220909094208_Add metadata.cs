using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdrequests_api.Migrations
{
    public partial class Addmetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServerId",
                table: "Levels",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_ServerId",
                table: "Levels",
                newName: "IX_Levels_LevelId");

            migrationBuilder.CreateTable(
                name: "LevelMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<int>(type: "INTEGER", nullable: false),
                    IsEpic = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: false),
                    LevelName = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDemon = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAuto = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelMetadata_Levels_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LevelMetadata_RequestId",
                table: "LevelMetadata",
                column: "RequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LevelMetadata");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Levels",
                newName: "ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_LevelId",
                table: "Levels",
                newName: "IX_Levels_ServerId");
        }
    }
}
