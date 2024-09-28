using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO
{
    public class RegisterModelDTO
    {
        [Required(ErrorMessage = "Atributo requerido.")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Atributo requerido.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Atributo requerido.")]
        public string? Password { get; set; }
    }
}
