using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories;
public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters)
    {
        var categoriasAsync = await GetAllAsync();
        var categoriasAsQueryable = categoriasAsync.OrderBy(c => c.CategoriaId).AsQueryable();

        var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categoriasAsQueryable, categoriasParameters.PageNumber, categoriasParameters.PageSize);

        return categoriasOrdenadas;
    }

    public async Task<PagedList<Categoria>> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNome)
    {
        var categorias = await GetAllAsync();
        var categoriasAsQueryble = categorias.AsQueryable();

        if (!string.IsNullOrEmpty(categoriasFiltroNome.Nome))
        {
            categoriasAsQueryble = categoriasAsQueryble.Where(c=>c.Nome.Contains(categoriasFiltroNome.Nome)).OrderBy(c=>c.Nome);
        }

        var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categoriasAsQueryble, categoriasFiltroNome.PageNumber, categoriasFiltroNome.PageSize);

        return categoriasFiltradas;
    }
}