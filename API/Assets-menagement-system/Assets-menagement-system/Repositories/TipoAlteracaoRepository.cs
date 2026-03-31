using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;
using System.Runtime.CompilerServices;

namespace Assets_menagement_system.Repositories
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly AssetMenagementDbContext _context;
        public TipoAlteracaoRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(t => t.NomeTipoAlteracao).ToList();
        }
        public TipoAlteracao ObterPorId(Guid guid)
        {
            return _context.TipoAlteracao.FirstOrDefault(t => t.TipoAlteracaoId == guid);
        }
        public TipoAlteracao ObterPorNome(string nome)
        {
            return _context.TipoAlteracao.FirstOrDefault(t => t.NomeTipoAlteracao == nome);
        }
        public bool TipoAlteracaoExiste(Guid? guid = null, string? nome = null)
        {
            return _context.TipoAlteracao.Any(t => t.TipoAlteracaoId == guid || t.NomeTipoAlteracao == nome);
        }
        public void Adicionar(TipoAlteracao tipoAlteracao)
        {
            TipoAlteracao tipoAlteracaoNovo = new TipoAlteracao
            {
                NomeTipoAlteracao = tipoAlteracao.NomeTipoAlteracao
            };
            _context.Add(tipoAlteracaoNovo);
            _context.SaveChanges();
        }
        public void Atualizar(Guid guid, TipoAlteracao tipoAlteracao)
        {
            TipoAlteracao tipoAlteracaoBanco = _context.TipoAlteracao.Find(guid);
            if (tipoAlteracaoBanco == null)
                return;
            tipoAlteracaoBanco.NomeTipoAlteracao = tipoAlteracao.NomeTipoAlteracao;
            _context.Update(tipoAlteracaoBanco);
            _context.SaveChanges();
        }
    }
}
