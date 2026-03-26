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
        public Area ObterPorNome(string nome)
        {
            return ctx.Area.FirstOrDefault(a => a.NomeArea == nome);
        }


        public void Adicionar(Area area)
        {
            ctx.Area.Add(area);
            ctx.SaveChanges();
        }

        public void Atualizar(Area area)
        {
            if (area == null)
                return;

            Area areaBanco = ctx.Area.FirstOrDefault(a => a.AreaId == area.AreaId);

            areaBanco.NomeArea = area.NomeArea;

            ctx.Area.Update(area);
            ctx.SaveChanges();
        }
    }
}
