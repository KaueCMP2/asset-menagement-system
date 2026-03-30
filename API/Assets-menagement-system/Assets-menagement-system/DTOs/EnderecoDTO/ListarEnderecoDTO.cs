using Assets_menagement_system.Domains;

namespace Assets_menagement_system.DTOs.EnderecoDTO
{
    public class ListarEnderecoDTO
    {
        public string Logradouro { get; set; } = null!;

        public int Numero { get; set; }

        public string Complemento { get; set; } = null!;

        public string CEP { get; set; } = null!;

        public Guid BairroId { get; set; }

        public virtual Bairro Bairro { get; set; } = null!;

        public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
    }
}
