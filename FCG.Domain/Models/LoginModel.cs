using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Senha { get; set; }
    }
}
