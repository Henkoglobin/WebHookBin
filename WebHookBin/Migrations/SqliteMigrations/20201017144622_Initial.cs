using Microsoft.EntityFrameworkCore.Migrations;

namespace WebHookBin.Migrations.SqliteMigrations {
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Method = table.Column<int>(type: "INTEGER", nullable: false),
                    RawBody = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Header",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    LogEntryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Header", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Header_LogEntries_LogEntryId",
                        column: x => x.LogEntryId,
                        principalTable: "LogEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QueryParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    LogEntryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueryParameter_LogEntries_LogEntryId",
                        column: x => x.LogEntryId,
                        principalTable: "LogEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Header_LogEntryId",
                table: "Header",
                column: "LogEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_QueryParameter_LogEntryId",
                table: "QueryParameter",
                column: "LogEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Header");

            migrationBuilder.DropTable(
                name: "QueryParameter");

            migrationBuilder.DropTable(
                name: "LogEntries");
        }
    }
}
