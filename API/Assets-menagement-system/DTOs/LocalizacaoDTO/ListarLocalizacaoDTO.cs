namespace Assets_menagement_system.DTOs.LocalizacaoDTO
{
    public class ListarLocalizacaoDTO
    {
        public Guid LocalizacaoId { get; set; }

        public string? NomeLocal { get; set; }

        public int? LocalSAP { get; set; }

        public string? DescricaoSAP { get; set; }

        public bool? StatusLocalizacao { get; set; }

        public Guid? AreaId { get; set; }
    }
}
