using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{

    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid patrimonioId);
        Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio);

        bool LocalizacaoExiste(Guid localizacaoId);
        bool StatusPatrimonioExiste(Guid statusPatrimonioId);

        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
        void AdicionarLog(Log_Patrimonio log_patrimonio);

        Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao);
        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);
        StatusPatrimonio BuscarStatusPatrimonioPorId(Guid statusPatrimonioId);
        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
    }
}