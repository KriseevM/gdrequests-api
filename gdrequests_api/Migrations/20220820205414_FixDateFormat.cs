using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gdrequests_api.Migrations
{
    public partial class FixDateFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddedAt",
                table: "Levels",
                type: "INTEGER",
                nullable: false,
                defaultValueSql: "UNIXEPOCH()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "UNIXEPOCH()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedAt",
                table: "Levels",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "UNIXEPOCH()",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValueSql: "UNIXEPOCH()");
        }
    }
}
