using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AssetMenagementDbContext _ctx;
        public AreaRepository(AssetMenagementDbContext context)
        {
            _ctx = context;
        }

        public List<Area> Listar()
        {
            return _ctx.Area.OrderBy(area => area.NomeArea).ToList();
        }

        public Area ObterPorId(Guid guid)
        {
            return _ctx.Area.FirstOrDefault(a => a.AreaId == guid);
        }
        public Area ObterPorNome(string nome)
        {
            return _ctx.Area.FirstOrDefault(a => a.NomeArea == nome);
        }
        public bool AreaExiste(string nome)
        {
            return _ctx.Area.Any(a => a.NomeArea == nome);
        }
        public void Adicionar(Area area)
        {
            _ctx.Area.Add(area);
            _ctx.SaveChanges();
        }
        public void Atualizar(Area area)
        {
            if (area == null)
                return;

            Area areaBanco = _ctx.Area.FirstOrDefault(a => a.AreaId == area.AreaId);

            areaBanco.NomeArea = area.NomeArea;

            _ctx.Area.Update(area);
            _ctx.SaveChanges();
        }
    }
}
