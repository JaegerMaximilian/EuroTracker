using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euro24Tracker.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Nation",
                table: "Nation");

            migrationBuilder.RenameTable(
                name: "Nation",
                newName: "Nationen");

            migrationBuilder.AddColumn<int>(
                name: "Gegentore",
                table: "Nationen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GruppeId",
                table: "Nationen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Punkte",
                table: "Nationen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tore",
                table: "Nationen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Torverhältnis",
                table: "Nationen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nationen",
                table: "Nationen",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EreignisTyp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageLink = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EreignisTyp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gruppen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gruppen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spiele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Stadion = table.Column<string>(type: "TEXT", nullable: false),
                    Gruppenphase = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spiele", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ereignisse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Minute = table.Column<int>(type: "INTEGER", nullable: true),
                    Kommentar = table.Column<string>(type: "TEXT", nullable: false),
                    SpielId = table.Column<int>(type: "INTEGER", nullable: false),
                    EreignisTypId = table.Column<int>(type: "INTEGER", nullable: true),
                    TorNationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ereignisse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ereignisse_EreignisTyp_EreignisTypId",
                        column: x => x.EreignisTypId,
                        principalTable: "EreignisTyp",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ereignisse_Nationen_TorNationId",
                        column: x => x.TorNationId,
                        principalTable: "Nationen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ereignisse_Spiele_SpielId",
                        column: x => x.SpielId,
                        principalTable: "Spiele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpielNationen",
                columns: table => new
                {
                    SpielId = table.Column<int>(type: "INTEGER", nullable: false),
                    NationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tore = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpielNationen", x => new { x.SpielId, x.NationId });
                    table.ForeignKey(
                        name: "FK_SpielNationen_Nationen_NationId",
                        column: x => x.NationId,
                        principalTable: "Nationen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpielNationen_Spiele_SpielId",
                        column: x => x.SpielId,
                        principalTable: "Spiele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nationen_GruppeId",
                table: "Nationen",
                column: "GruppeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ereignisse_EreignisTypId",
                table: "Ereignisse",
                column: "EreignisTypId");

            migrationBuilder.CreateIndex(
                name: "IX_Ereignisse_SpielId",
                table: "Ereignisse",
                column: "SpielId");

            migrationBuilder.CreateIndex(
                name: "IX_Ereignisse_TorNationId",
                table: "Ereignisse",
                column: "TorNationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpielNationen_NationId",
                table: "SpielNationen",
                column: "NationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nationen_Gruppen_GruppeId",
                table: "Nationen",
                column: "GruppeId",
                principalTable: "Gruppen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nationen_Gruppen_GruppeId",
                table: "Nationen");

            migrationBuilder.DropTable(
                name: "Ereignisse");

            migrationBuilder.DropTable(
                name: "Gruppen");

            migrationBuilder.DropTable(
                name: "SpielNationen");

            migrationBuilder.DropTable(
                name: "EreignisTyp");

            migrationBuilder.DropTable(
                name: "Spiele");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nationen",
                table: "Nationen");

            migrationBuilder.DropIndex(
                name: "IX_Nationen_GruppeId",
                table: "Nationen");

            migrationBuilder.DropColumn(
                name: "Gegentore",
                table: "Nationen");

            migrationBuilder.DropColumn(
                name: "GruppeId",
                table: "Nationen");

            migrationBuilder.DropColumn(
                name: "Punkte",
                table: "Nationen");

            migrationBuilder.DropColumn(
                name: "Tore",
                table: "Nationen");

            migrationBuilder.DropColumn(
                name: "Torverhältnis",
                table: "Nationen");

            migrationBuilder.RenameTable(
                name: "Nationen",
                newName: "Nation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nation",
                table: "Nation",
                column: "Id");
        }
    }
}
