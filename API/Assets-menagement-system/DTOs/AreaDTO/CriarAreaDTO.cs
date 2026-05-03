using System.ComponentModel.DataAnnotations;

namespace Assets_menagement_system.DTOs.AreaDTO
{
    public class CriarAreaDTO
    {
        [Required(ErrorMessage = "O nome da área é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da área deve ter no máximo 50 caracteres.")]
        public string NomeArea { get; set; } = string.Empty;

    }
}
