using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly AssetMenagementDbContext _context;
        public TipoPatrimonioRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(t => t.NomeTipo).ToList();
        }
        public TipoPatrimonio ObterPorId(Guid guid)
        {
            return _context.TipoPatrimonio.FirstOrDefault(t => t.TipoPatrimonioId == guid);
        }
        public TipoPatrimonio ObterPorNome(string nome)
        {
            return _context.TipoPatrimonio.FirstOrDefault(t => t.NomeTipo == nome);
        }
        public bool TipoPatrimonioExiste(Guid? guid = null, string? nome = null)
        {
            return _context.TipoPatrimonio.Any(t => t.TipoPatrimonioId == guid || t.NomeTipo == nome);
        }
        public void Adicionar(TipoPatrimonio tipo)
        {
            TipoPatrimonio tipoNovo = new TipoPatrimonio
            {
                NomeTipo = tipo.NomeTipo
            };
            _context.Add(tipoNovo);
            _context.SaveChanges();
        }
        public void Atualizar(Guid guid, TipoPatrimonio tipo)
        {
            TipoPatrimonio tipoBanco = _context.TipoPatrimonio.Find(guid);
            if (tipoBanco == null)
                return;
            tipoBanco.NomeTipo = tipo.NomeTipo;
            _context.Update(tipoBanco);
            _context.SaveChanges();
        }
    }
}
