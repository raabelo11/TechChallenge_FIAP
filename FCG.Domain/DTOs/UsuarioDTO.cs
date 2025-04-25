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
        public required string Email { get; set; }

        [Required]
        public required string Senha { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; }
    }
}
