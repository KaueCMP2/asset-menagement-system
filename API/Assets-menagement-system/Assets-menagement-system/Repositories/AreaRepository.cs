using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AssetMenagementDbContext ctx;
        public AreaRepository(AssetMenagementDbContext _ctx)
        {
            ctx = _ctx;
        }

        public List<Area> Listar()
        {
            return ctx.Area.OrderBy(area => area.NomeArea).ToList();
        }

        public Area ObterPorId(Guid guid)
        {
            return ctx.Area.FirstOrDefault(a => a.AreaId == guid);
        }
    }
}
