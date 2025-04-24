using FCG.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.Models
{
    public class Jogos
    {
        [Key]
        public Guid IdJogo { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "O nome do jogo é obrigatório!")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "A descrição do jogo é obrigatório!")]
        [StringLength(200)]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        [Range(10, 500, ErrorMessage = "Valor inválido")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Inserir categoria do jogo")]
        public Generos Categoria { get; set; }
    }
}
