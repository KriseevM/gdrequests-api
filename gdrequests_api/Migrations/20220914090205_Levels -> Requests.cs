using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdrequests_api.Migrations
{
    public partial class LevelsRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelMetadata_Levels_RequestId",
                table: "LevelMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levels",
                table: "Levels");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Requests");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_LevelId",
                table: "Requests",
                newName: "IX_Requests_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_AddedAt",
                table: "Requests",
                newName: "IX_Requests_AddedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelMetadata_Requests_RequestId",
                table: "LevelMetadata",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelMetadata_Requests_RequestId",
                table: "LevelMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Levels");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_LevelId",
                table: "Levels",
                newName: "IX_Levels_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_AddedAt",
                table: "Levels",
                newName: "IX_Levels_AddedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levels",
                table: "Levels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelMetadata_Levels_RequestId",
                table: "LevelMetadata",
                column: "RequestId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
