namespace Assets_menagement_system.DTOs.UsuarioDTO
{
    public class CriarUsuarioDTO
    {
        public Guid UsuarioId { get; set; }

        public string NIF { get; set; } = null!;

        public string NomeUsuario { get; set; } = null!;

        public string RG { get; set; } = null!;

        public string CPF { get; set; } = null!;

        public string? CarteiraDeTabalho { get; set; }

        public string Email { get; set; } = null!;

        public Guid EnderecoId { get; set; }

        public Guid CargoId { get; set; }

        public Guid TipoUsuarioId { get; set; }
    }
}
