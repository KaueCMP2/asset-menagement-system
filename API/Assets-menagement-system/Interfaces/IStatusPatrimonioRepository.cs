using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface IStatusPatrimonioRepository
    {
        public List<StatusPatrimonio> Listar();
        public StatusPatrimonio ObterPorId(Guid guid);
        public StatusPatrimonio ObterPorNome(string nome);
        public bool StatusExiste(Guid? guid = null, string? nome = null);
        public void Adicionar(StatusPatrimonio status);
        public void Atualizar(Guid guid, StatusPatrimonio status);
    }
}
