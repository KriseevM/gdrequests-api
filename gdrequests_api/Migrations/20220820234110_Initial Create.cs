using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdrequests_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<int>(type: "INTEGER", nullable: false, defaultValueSql: "strftime('%s', 'now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_AddedAt",
                table: "Levels",
                column: "AddedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_ServerId",
                table: "Levels",
                column: "ServerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Levels");
        }
    }
}
