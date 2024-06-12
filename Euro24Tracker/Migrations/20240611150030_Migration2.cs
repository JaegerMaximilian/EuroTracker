using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euro24Tracker.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpielNationen_Nationen_NationId",
                table: "SpielNationen");

            migrationBuilder.DropForeignKey(
                name: "FK_SpielNationen_Spiele_SpielId",
                table: "SpielNationen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpielNationen",
                table: "SpielNationen");

            migrationBuilder.RenameTable(
                name: "SpielNationen",
                newName: "SpielNation");

            migrationBuilder.RenameIndex(
                name: "IX_SpielNationen_NationId",
                table: "SpielNation",
                newName: "IX_SpielNation_NationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpielNation",
                table: "SpielNation",
                columns: new[] { "SpielId", "NationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpielNation_Nationen_NationId",
                table: "SpielNation",
                column: "NationId",
                principalTable: "Nationen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpielNation_Spiele_SpielId",
                table: "SpielNation",
                column: "SpielId",
                principalTable: "Spiele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpielNation_Nationen_NationId",
                table: "SpielNation");

            migrationBuilder.DropForeignKey(
                name: "FK_SpielNation_Spiele_SpielId",
                table: "SpielNation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpielNation",
                table: "SpielNation");

            migrationBuilder.RenameTable(
                name: "SpielNation",
                newName: "SpielNationen");

            migrationBuilder.RenameIndex(
                name: "IX_SpielNation_NationId",
                table: "SpielNationen",
                newName: "IX_SpielNationen_NationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpielNationen",
                table: "SpielNationen",
                columns: new[] { "SpielId", "NationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpielNationen_Nationen_NationId",
                table: "SpielNationen",
                column: "NationId",
                principalTable: "Nationen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpielNationen_Spiele_SpielId",
                table: "SpielNationen",
                column: "SpielId",
                principalTable: "Spiele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
