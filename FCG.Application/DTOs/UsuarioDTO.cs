using FCG.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$", ErrorMessage = "E-mail inválido")]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=\[{\]};:'"",<.>/?\\|`~]).{8,}$",
        ErrorMessage = "A senha deve ter no mínimo 8 caracteres, com letras, números e caracteres especiais.")]
        public required string Senha { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }
    }
}
