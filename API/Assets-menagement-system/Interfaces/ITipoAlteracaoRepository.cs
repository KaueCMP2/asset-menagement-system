using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface ITipoAlteracaoRepository
    {
        public List<TipoAlteracao> Listar();
        public TipoAlteracao ObterPorId(Guid guid);
        public TipoAlteracao ObterPorNome(string nome);
        public bool TipoAlteracaoExiste(Guid? guid = null, string? nome = null);
        public void Adicionar(TipoAlteracao tipoAlteracao);
        public void Atualizar(Guid guid, TipoAlteracao tipoAlteracao);
    }
}
