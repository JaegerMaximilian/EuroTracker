using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euro24Tracker.Migrations
{
    /// <inheritdoc />
    public partial class Migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spieler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Tore = table.Column<int>(type: "INTEGER", nullable: false),
                    NationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spieler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spieler_Nationen_NationId",
                        column: x => x.NationId,
                        principalTable: "Nationen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spieler_NationId",
                table: "Spieler",
                column: "NationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spieler");
        }
    }
}
