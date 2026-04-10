namespace Assets_menagement_system.DTOs.PatrimonioDTO
{
    public class ImportarPatrimonioCsvDTO
    {
        public int NumeroSerie { get; set; }
        public string Denominacao { get; set; } = string.Empty;
        public DateTime DataImcorporacao { get; set; }
        public decimal Valor { get; set; }

        public string? Imagem { get; set; }
        public Guid LocalizacaoId { get; set; }
        public Guid StatusPatrimonioId { get; set; }
    }
}
