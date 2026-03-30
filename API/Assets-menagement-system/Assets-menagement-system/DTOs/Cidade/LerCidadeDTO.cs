namespace Assets_menagement_system.DTOs.Cidade
{
    public class LerCidadeDTO
    {
        public Guid CidadeId { get; set; }

        public string NomeCidade { get; set; } = null!;

        public string NomeEstado { get; set; } = null!;
    }
}
