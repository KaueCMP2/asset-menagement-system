using Assets_menagement_system.Domains;

namespace Assets_menagement_system.DTOs.BairroDto
{
    public class LerBairroDTO
    {
        public Guid BairroId { get; set; }

        public string NomeBairro { get; set; } = null!;

        public Guid CidadeId { get; set; }

    }
}
