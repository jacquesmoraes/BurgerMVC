using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMVC.Migrations
{
    /// <inheritdoc />
    public partial class ItensCarrinhoCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoComprasItens",
                columns: table => new
                {
                    ItensId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    LancheId = table.Column<int>(type: "int", nullable: true),
                    CarrinhoDeComprasId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoComprasItens", x => x.ItensId);
                    table.ForeignKey(
                        name: "FK_CarrinhoComprasItens_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "LancheId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoComprasItens_LancheId",
                table: "CarrinhoComprasItens",
                column: "LancheId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoComprasItens");
        }
    }
}
