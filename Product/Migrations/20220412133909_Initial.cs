using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dobavljac",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dobavljac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JedinicaMere",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JedinicaMere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipProizvoda",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipProizvoda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    Pdv = table.Column<double>(type: "float", nullable: false),
                    TipProizvodaId = table.Column<long>(type: "bigint", nullable: false),
                    JedinicaMereId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proizvod_JedinicaMere_JedinicaMereId",
                        column: x => x.JedinicaMereId,
                        principalTable: "JedinicaMere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvod_TipProizvoda_TipProizvodaId",
                        column: x => x.TipProizvodaId,
                        principalTable: "TipProizvoda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProizvodDobavljac",
                columns: table => new
                {
                    ProizvodId = table.Column<long>(type: "bigint", nullable: false),
                    DobavljacId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProizvodDobavljac", x => new { x.DobavljacId, x.ProizvodId });
                    table.ForeignKey(
                        name: "FK_ProizvodDobavljac_Dobavljac_DobavljacId",
                        column: x => x.DobavljacId,
                        principalTable: "Dobavljac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProizvodDobavljac_Proizvod_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dobavljac",
                columns: new[] { "Id", "Napomena", "Naziv", "PIB" },
                values: new object[,]
                {
                    { 1L, "Napomena....", "Dobavljac 1", "1221223344" },
                    { 2L, null, "Dobavljac 2", "9874512" },
                    { 3L, null, "Dobavljac 3", "23974524123" }
                });

            migrationBuilder.InsertData(
                table: "JedinicaMere",
                columns: new[] { "Id", "Naziv" },
                values: new object[,]
                {
                    { 1L, "Kilogram" },
                    { 2L, "Kilogram" },
                    { 3L, "Gram" },
                    { 4L, "Miligram" }
                });

            migrationBuilder.InsertData(
                table: "TipProizvoda",
                columns: new[] { "Id", "Naziv" },
                values: new object[,]
                {
                    { 1L, "Tip 1" },
                    { 2L, "Tip 2" },
                    { 3L, "Tip 3" },
                    { 4L, "Tip 4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_JedinicaMereId",
                table: "Proizvod",
                column: "JedinicaMereId");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_TipProizvodaId",
                table: "Proizvod",
                column: "TipProizvodaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodDobavljac_ProizvodId",
                table: "ProizvodDobavljac",
                column: "ProizvodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProizvodDobavljac");

            migrationBuilder.DropTable(
                name: "Dobavljac");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "JedinicaMere");

            migrationBuilder.DropTable(
                name: "TipProizvoda");
        }
    }
}
