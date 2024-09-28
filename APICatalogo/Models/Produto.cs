// Scoped namespace
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

/*
 * Classe anêmica = classe sem comportamento
 */
[Table("Produtos")]
public class Produto
{
    /*
     * Identificador Id no nome deste atributo seguindo convenção do EF Core
     * que permite o correto mapeamento deste atributo como PK da entidade
     */
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName="decimal(10,2)")]
    public decimal Preco{ get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque{ get; set; }
    public DateTime DataCadastro { get; set; }

    /*
     * Propriedades de navegação
     */
    public int? CategoriaId { get; set; }

    // Atributo SEMPRE será ignorado quando for serializado
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public Categoria? Categoria { get; set; }
}
