namespace Assets_menagement_system.DTOs.LocalizacaoDTO
{
    public class CriarLocalizacaoDTO
    {
        public string? NomeLocal { get; set; }

        public int LocalSAP { get; set; }

        public string? DescricaoSAP { get; set; }

        public Guid? AreaId { get; set; }
    }
}
