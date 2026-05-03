using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<Log_Patrimonio> Listar();
        List<Log_Patrimonio> ObterPorPatrimonio(Guid patrimonioId);
    }
}
