namespace Assets_menagement_system.DTOs.PatrimonioDTO
{
    public class ImportarPatrimonioCsvDTO
    {
        public string NumeroSerie { get; set; }
        public string Denominacao { get; set; } = string.Empty;
        public string DataIncorporacao { get; set; }
        public string Valor { get; set; }

        public string? Imagem { get; set; }
        public Guid LocalizacaoId { get; set; }
        public Guid StatusPatrimonioId { get; set; }
    }
}
