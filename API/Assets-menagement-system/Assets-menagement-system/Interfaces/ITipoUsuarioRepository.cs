using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        public List<TipoUsuario> Listar();
        public TipoUsuario ObterPorId(Guid guid);
        public TipoUsuario ObterPorNome(string nome);
        public bool TipoExiste(Guid? guid = null, string? nome = null);
        public void Adicionar(TipoUsuario tipoUsuario);
        public void Atualizar(Guid guid,TipoUsuario tipoUsuario);
    }
}
