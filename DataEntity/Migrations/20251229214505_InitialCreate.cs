using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Naradi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vykon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Umisteni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hmotnost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poznamka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CenaZaDen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dostupnost = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naradi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zakaznici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organizace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poznamka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ban = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zakaznici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vypujcky",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZakaznikId = table.Column<int>(type: "int", nullable: false),
                    NaradiId = table.Column<int>(type: "int", nullable: false),
                    DatumVypujcky = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumVraceniPlan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumVraceniSkutecne = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Penale = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vypujcky", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vypujcky_Naradi_NaradiId",
                        column: x => x.NaradiId,
                        principalTable: "Naradi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vypujcky_Zakaznici_ZakaznikId",
                        column: x => x.ZakaznikId,
                        principalTable: "Zakaznici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vypujcky_NaradiId",
                table: "Vypujcky",
                column: "NaradiId");

            migrationBuilder.CreateIndex(
                name: "IX_Vypujcky_ZakaznikId",
                table: "Vypujcky",
                column: "ZakaznikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vypujcky");

            migrationBuilder.DropTable(
                name: "Naradi");

            migrationBuilder.DropTable(
                name: "Zakaznici");
        }
    }
}
