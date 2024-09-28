using APICatalogo.Context;
using APICatalogo.DTO;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly ILogger _log;

    public ProdutosController(IUnitOfWork uow, IMapper mapper, IConfiguration config, ILogger<ProdutosController> log)
    {
        _uow = uow;
        _mapper = mapper;
        _config = config;
        _log = log;
    }

    [EnableCors("_origensComAcessPermitido")]
    [HttpGet("GetConnectionString")]
    public ActionResult<string?> GetConnectionString()
    {
        _log.LogInformation("***********************************DEBUG MESSAGE***********************************");
        return _config["ConnectionStrings:DefaultConnection"];
    }

    [HttpGet]
    [HttpGet("teste")]
    [HttpGet("/ObterListaProduto")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        /* 
         * Método de extensão AsNoTracking() impede que os dados retornados sejam armazenados em cache
         * o que melhora a performance do programa
         * Deve-se utilizar para consultas somente leituras
         * quando não há necessidade de alterar os dados
         */

        // Para materializar a consulta usa-se o método ToList
        var produtos = await _uow.ProdutoRepository.GetAllAsync();

        if (produtos is null)
            return NotFound("Produtos não encontrados.");

        // var destino = _mapper.Map<DESTINO>(ORIGEM);
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    // Definição de rota nomeada
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get([FromRoute] int id)
    {
        var produto = await _uow.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound($"Produto ID {id} não encontrado.");

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> Post([FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDTO);

        var novoProduto = _uow.ProdutoRepository.Create(produto);
        await _uow.CommitAsync();

        var novoProdutoDTO = _mapper.Map<ProdutoDTO>(novoProduto);
        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
    }

    /* A diferença entre os métodos HTTP Put e Patch está no modo como são esperados os parâmetros.
    *  No caso do método PUT deve-se enviar o objeto com todos os dados preenchidos
    *  Enquanto no método PATCH pode-se realizar a atualização do objeto com seu preenchimento parcial
    *  
    *  Pode-se retornar os status code http 200 OK ou 204 NoContent, quando houve o processo porém não está sendo retornado nenhuma informação
    */

    // Composição do template de rota com restrição do tipo
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Put([FromRoute] int id, [FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO.ProdutoId != id)
            return BadRequest();

        /* Informar ao contexto que a entidade 'Produto' está em estado modificado
         * Devido ao cenário desconectado, onde os dados do banco de dados estão em memória
         */

        var produto = _mapper.Map<Produto>(produtoDTO);
        //_context.Entry(produto).State = EntityState.Modified;
        var produtoAtualizado = _uow.ProdutoRepository.Update(produto);
        await _uow.CommitAsync();

        var produtoDTOAtualizado = _mapper.Map<ProdutoDTO>(produtoAtualizado);
        return Ok(produtoDTOAtualizado);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete([FromRoute] int id)
    {
        var produto = await _uow.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
            return BadRequest("Produto não encontrado.");

        var produtoDeletado = _uow.ProdutoRepository.Delete(produto);
        await _uow.CommitAsync();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);
        return Ok(produtoDeletado);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
    {
        var produtos = await _uow.ProdutoRepository.GetProdutos(produtosParameters);

        return ObterProdutos(produtos);
    }

    [HttpGet("filter/preco/pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
    {
        var produtos = await _uow.ProdutoRepository.GetProdutosFiltroPreco(produtosFiltroPreco);

        return ObterProdutos(produtos);
    }

    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(PagedList<Produto> produtos)
    {
        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDTO);
    }


    [HttpPatch("{id}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id,
        JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO is null || id <= 0)
            return BadRequest();

        var produto = await _uow.ProdutoRepository.GetAsync(c => c.ProdutoId == id);

        if (produto is null)
            return NotFound();

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);
        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            return BadRequest();

        _mapper.Map(produtoUpdateRequest, produto);
        _uow.ProdutoRepository.Update(produto);
        await _uow.CommitAsync();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }
}