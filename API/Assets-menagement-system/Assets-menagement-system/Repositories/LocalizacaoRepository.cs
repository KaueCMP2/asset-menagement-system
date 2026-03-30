using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly AssetMenagementDbContext _ctx;

        public LocalizacaoRepository(AssetMenagementDbContext _context)
        {
            _ctx = _context;
        }

        // C#
        public List<Localizacao> Listar()
        {
            return _ctx.Localizacao
                      .OrderBy(loc => loc.NomeLocal)
                      .ToList();
        }
        public Localizacao ObterPorId(Guid guid)
        {
            return _ctx.Localizacao.Find(guid);
        }
        public Localizacao ObterPorNome(string nome, Guid areaId)
        {
            return _ctx.Localizacao.FirstOrDefault(loc => loc.NomeLocal == nome && loc.AreaId == areaId);
        }
        public bool LocalExiste(string nome)
        {
            return _ctx.Localizacao.Any(l => l.NomeLocal == nome);
        }
        public bool AreaExiste(Guid guid)
        {
            return _ctx.Area.Any(a => a.AreaId == guid);
        }
        public void Adicionar(Localizacao localizacao)
        {
            _ctx.Localizacao.Add(localizacao);
            _ctx.SaveChanges();
        }
        public void Atualizar(Localizacao localizacao)
        {
            if (localizacao == null)
                return;

            Localizacao locBanco = _ctx.Localizacao.FirstOrDefault(loc => loc.LocalizacaoId == localizacao.LocalizacaoId);

            _ctx.Localizacao.Update(locBanco);
            _ctx.SaveChanges();
        }
    }
}
