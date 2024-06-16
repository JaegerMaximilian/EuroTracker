using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euro24Tracker.Migrations
{
    /// <inheritdoc />
    public partial class Migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TorschuetzeId",
                table: "Ereignisse",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ereignisse_TorschuetzeId",
                table: "Ereignisse",
                column: "TorschuetzeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ereignisse_Spieler_TorschuetzeId",
                table: "Ereignisse",
                column: "TorschuetzeId",
                principalTable: "Spieler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ereignisse_Spieler_TorschuetzeId",
                table: "Ereignisse");

            migrationBuilder.DropIndex(
                name: "IX_Ereignisse_TorschuetzeId",
                table: "Ereignisse");

            migrationBuilder.DropColumn(
                name: "TorschuetzeId",
                table: "Ereignisse");
        }
    }
}
