using APICatalogo.DTO;
using APICatalogo.DTO.Mappings;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CategoriasController([FromServices] IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            try
            {
                /*
                 * Nunca retornar todos os objetos em uma consulta
                 * Utilizar sempre que possível ao menos um filtro
                 */
                var categorias = await _uow.CategoriaRepository.GetAllAsync();

                var categoriasDTO = categorias.ToCategoriaDTOList();

                // return of status code 200
                return Ok(categoriasDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            //var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            var categoria = await _uow.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrada.");

            var categoriaDTO = categoria.ToCategoriaDTO();

            return Ok(categoriaDTO);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post([FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
                return BadRequest("Categoria não está preenchida.");

            var categoria = categoriaDTO.ToCategoria();

            var categoriaCriada = _uow.CategoriaRepository.Create(categoria);
            _uow.CommitAsync();

            var categoriaDTOCriada = categoriaCriada.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaDTOCriada.CategoriaId }, categoriaDTOCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put([FromRoute] int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO.CategoriaId != id)
                return BadRequest("IDs diferentes.");

            var categoria = categoriaDTO.ToCategoria();

            var categoriaAtualizada = _uow.CategoriaRepository.Update(categoria);
            _uow.CommitAsync();

            var categoriaDTOAtualizada = categoriaAtualizada.ToCategoriaDTO();

            return Ok(categoriaDTOAtualizada);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete([FromRoute] int id)
        {
            var categoria = await _uow.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrada.");

            var categoriaDeletada = _uow.CategoriaRepository.Delete(categoria);

            var categoriaDTO = categoriaDeletada.ToCategoriaDTO();

            return Ok(categoriaDeletada);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _uow.CategoriaRepository.GetCategoriasAsync(categoriasParameters);

            return ObterCategorias(categorias);
        }

        [HttpGet("filter/nome/pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasFiltradas(CategoriasFiltroNome categoriasFiltroNome)
        {
            var categoriasFiltradas = await _uow.CategoriaRepository.GetCategoriasFiltroNome(categoriasFiltroNome);

            return (ObterCategorias(categoriasFiltradas));
        }

        private ActionResult<IEnumerable<CategoriaDTO>> ObterCategorias(PagedList<Categoria> categorias)
        {
            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriasDTO = categorias.ToCategoriaDTOList();

            return Ok(categoriasDTO);
        }
    }
}
