using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly AssetMenagementDbContext _context;
        public TipoUsuarioRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }
        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.OrderBy(t => t.Nome).ToList();
        }
        public TipoUsuario ObterPorId(Guid guid)
        {
            return _context.TipoUsuario.Find(guid);
        }
        public TipoUsuario ObterPorNome(string nome)
        {
            return _context.TipoUsuario.FirstOrDefault(t => t.Nome == nome);
        }
        public bool TipoExiste(Guid? guid = null, string? nome = null)
        {
            return _context.TipoUsuario.Any(t => t.TipoUsuarioId == guid || t.Nome == nome);
        }
        public void Adicionar(TipoUsuario tipoUsuario)
        {
            TipoUsuario tipoNovo = new TipoUsuario
            {
                Nome = tipoUsuario.Nome
            };

            _context.Add(tipoNovo);
            _context.SaveChanges();
        }
        public void Atualizar(Guid guid, TipoUsuario tipoUsuario)
        {
            TipoUsuario tipoBanco = _context.TipoUsuario.Find(guid);
            if (tipoBanco == null)
                return;

            tipoBanco.Nome = tipoUsuario.Nome;

            _context.Update(tipoBanco);
            _context.SaveChanges();
        }
    }
}
