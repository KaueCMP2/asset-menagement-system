using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface ITipoPatrimonioRepository
    {
        public List<TipoPatrimonio> Listar();
        public TipoPatrimonio ObterPorId(Guid guid);
        public TipoPatrimonio ObterPorNome(string nome);
        public bool TipoPatrimonioExiste(Guid? guid = null, string? nome = null);
        public void Adicionar(TipoPatrimonio tipoPatrimonio);
        public void Atualizar(Guid guid, TipoPatrimonio tipoPatrimonio);
    }
}
