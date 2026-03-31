using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Listar();
        //public Usuario ObterPorId(Guid guid);
        //public Usuario ObterPorEmail(string email);
        //public bool UsuarioExiste(Guid? guid = null, string? email = null);
        //public void Adicionar(Usuario usuario);
        //public void Atualizar(Guid guid, Usuario usuario);
    }
}
