using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.Migrations
{
    public partial class InitialCreate : Migration
    {
        //this i migration file, steps performed on database when I do update
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proizvod");
        }
    }
}
