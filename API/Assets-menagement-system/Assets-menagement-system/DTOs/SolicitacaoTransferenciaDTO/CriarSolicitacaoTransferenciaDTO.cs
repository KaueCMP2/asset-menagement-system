namespace Assets_menagement_system.DTOs.SolicitacaoTransferenciaDTO
{
    public class CriarSolicitacaoTransferenciaDTO
    {
        public string Justificativa { get; set; } = string.Empty;
        public Guid PatrimonioID { get; set; }
        public Guid LocalizacaoID { get; set; }
    }
}
