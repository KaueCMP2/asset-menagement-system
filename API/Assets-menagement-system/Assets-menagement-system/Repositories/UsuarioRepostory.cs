using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class UsuarioRepostory : IUsuarioRepository
    {
        private readonly AssetMenagementDbContext _context;
        public UsuarioRepostory(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(u => u.NomeUsuario).ToList();
        }


    }
}
