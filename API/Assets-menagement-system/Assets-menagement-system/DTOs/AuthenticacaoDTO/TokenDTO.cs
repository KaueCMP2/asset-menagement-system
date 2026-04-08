namespace Assets_menagement_system.DTOs.AutenticacaoDTO
{
    public class TokenDTO
    {
        public string Token { get; set; } = string.Empty;
        public bool PrimeiroAcesso { get; set; }

        public string TipoUsuario { get; set; } = string.Empty;
    }
}
