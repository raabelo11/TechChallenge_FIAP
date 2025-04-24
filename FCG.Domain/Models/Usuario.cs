using FCG.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.Models
{
    public class Usuario
    {
        [Key]
        public Guid IdUsuario { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        public TipoUsuario Tipo { get; set; }
    }
}
