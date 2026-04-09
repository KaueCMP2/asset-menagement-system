namespace Assets_menagement_system.DTOs.SolicitacaoTransferenciaDTO
{
    public class LerSolicitacaoTransferenciaDTO
    {
        public Guid SolicitacaoId { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public DateTime? DataResposta { get; set; }
        public string Justificativa { get; set; } = string.Empty;
        public Guid StatusTransferenciaId { get; set; }
        public Guid UsuarioSolicitacaoId { get; set; }
        public Guid? UsuarioAprovacaoId { get; set; }
        public Guid PatrimonioId { get; set; }
        public Guid LocalizacaoId { get; set; }
    }
}
