using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                            "VALUES('Coca-Cola Diet', 'Refrigerante de Cola 350ml', 5.45, 'cocacoladiet.jpg', 50, GETDATE(), 1);");

            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                            "VALUES('Pepsi', 'Refrigerante Pepsi KS', 7.85, 'pepsiks.jpg', 27, GETDATE(), 1);");

            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                            "VALUES('X-Tudo', 'X-Tudo caprichado para quem tem fome', 30.99, 'xtudo.jpg', 35, GETDATE(), 2);");

            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "VALUES('X-Burguer', 'X-Burguer matador de fome', 18.99, 'xburguer.jpg', 23, GETDATE(), 2);");

            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                            "VALUES('Pudim de chocolate', 'Sobremesa de pudim de chocolate cremoso', 10.45, 'pudimchocolate.jpg', 40, GETDATE(), 3);");

            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                            "VALUES('Pudim de leite condensado', 'Pudim de leite condensado feito com leite moça', 11.45, 'pudimleitemoca.jpg', 30, GETDATE(), 3);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos;");
        }
    }
}
