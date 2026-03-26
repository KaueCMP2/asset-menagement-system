using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly AssetMenagementDbContext ctx;

        public LocalizacaoRepository(AssetMenagementDbContext _context)
        {
            ctx = _context;
        }

        // C#
        public List<Localizacao> Listar()
        {
            return ctx.Localizacao
                      .OrderBy(loc => loc.NomeLocal)
                      .ToList();
        }
        public Localizacao ObterPorId(Guid guid)
        {
            return ctx.Localizacao.Find(guid);
        }
        public Localizacao ObterPorNome(string nome)
        {
            return ctx.Localizacao.FirstOrDefault(loc => loc.NomeLocal == nome);
        }
        public bool AreaExiste(Guid? guid)
        {
            return ctx.Area.Any(a => a.AreaId == guid);
        }
        public void Adicionar(Localizacao localizacao)
        {
            ctx.Localizacao.Add(localizacao);
            ctx.SaveChanges();
        }
        public void Atualizar(Localizacao localizacao)
        {
            if (localizacao == null)
                return;

            Localizacao locBanco = ctx.Localizacao.FirstOrDefault(loc => loc.LocalizacaoId == localizacao.LocalizacaoId);

            ctx.Localizacao.Update(locBanco);
            ctx.SaveChanges();
        }
    }
}
