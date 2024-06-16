using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euro24Tracker.Migrations
{
    /// <inheritdoc />
    public partial class Migration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spieler_Nationen_NationId",
                table: "Spieler");

            migrationBuilder.AlterColumn<int>(
                name: "NationId",
                table: "Spieler",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Spieler_Nationen_NationId",
                table: "Spieler",
                column: "NationId",
                principalTable: "Nationen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spieler_Nationen_NationId",
                table: "Spieler");

            migrationBuilder.AlterColumn<int>(
                name: "NationId",
                table: "Spieler",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Spieler_Nationen_NationId",
                table: "Spieler",
                column: "NationId",
                principalTable: "Nationen",
                principalColumn: "Id");
        }
    }
}
