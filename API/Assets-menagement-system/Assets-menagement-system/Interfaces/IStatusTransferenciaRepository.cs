using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface IStatusTransferenciaRepository
    {
        public List<StatusTransferencia> Listar();
        public StatusTransferencia ObterPorId(Guid guid);
        public StatusTransferencia ObterPorNome(string nome);
    }
}
