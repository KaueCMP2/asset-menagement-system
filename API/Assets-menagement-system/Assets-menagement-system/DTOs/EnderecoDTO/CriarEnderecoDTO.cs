namespace Assets_menagement_system.DTOs.EnderecoDTO
{
    public class CriarEnderecoDTO
    {
        public string Logradouro { get; set; } = null!;

        public int Numero { get; set; }

        public string Complemento { get; set; } = null!;

        public string CEP { get; set; } = null!;

        public Guid BairroId { get; set; }
    }
}
