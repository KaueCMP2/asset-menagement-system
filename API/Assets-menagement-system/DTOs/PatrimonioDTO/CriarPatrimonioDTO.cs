namespace Assets_menagement_system.DTOs.PatrimonioDTO
{
    public class CriarPatrimonioDTO
    {
        public string? Denominacao { get; set; }

        public string NumeroSerie { get; set; }

        public decimal? Valor { get; set; }

        public string? Imagem { get; set; }

        public Guid LocalizacaoId { get; set; }

        public Guid StatusPatrimonioId { get; set; }
    }
}
