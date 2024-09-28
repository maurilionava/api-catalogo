using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories;
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    //public async Task<IEnumerable<Produto>> GetProdutos(ProdutosParameters produtosParameters)
    //{
    //    var produtos = await GetAllAsync();
    //    return produtos.OrderBy(p => p.Nome)
    //        .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
    //        .Take(produtosParameters.PageSize).ToList();
    //}

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();
        return produtos.Where(p => p.CategoriaId == id);
    }

    public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
    {
        var produtos = await GetAllAsync();
        var produtosAsQueryable = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtosAsQueryable, produtosParameters.PageNumber, produtosParameters.PageSize);
        return produtosOrdenados;
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco)
    {
        var produtos = await GetAllAsync();
        var produtosAsQueryable = produtos.AsQueryable();

        if (produtosFiltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroPreco.PrecoCriterio))
        {
            if (produtosFiltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtosAsQueryable = produtosAsQueryable.Where(p => p.Preco > produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtosAsQueryable = produtosAsQueryable.Where(p => p.Preco < produtosFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            } else if(produtosFiltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtosAsQueryable = produtosAsQueryable.Where(p=>p.Preco == produtosFiltroPreco.Preco.Value).OrderBy(p=>p.Preco);
            }
        }

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtosAsQueryable, produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);
        return produtosFiltrados;
    }
}
