using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHookBin.Migrations.SqliteMigrations
{
    public partial class AddPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "LogEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "LogEntries");
        }
    }
}
