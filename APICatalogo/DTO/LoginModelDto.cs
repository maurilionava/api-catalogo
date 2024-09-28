using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTO
{
    public class LoginModelDto
    {
        [Required(ErrorMessage = "Atributo requerido.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Atributo requerido.")]
        public string? Password { get; set; }
    }
}
