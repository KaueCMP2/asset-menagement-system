namespace Assets_menagement_system.DTOs.BairroDto
{
    public class CriarBairroDTO
    {
        public string NomeBairro { get; set; } = null!;

        public Guid CidadeId { get; set; }
    }
}
