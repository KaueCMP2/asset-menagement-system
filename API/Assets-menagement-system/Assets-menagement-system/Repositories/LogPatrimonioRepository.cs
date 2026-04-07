using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assets_menagement_system.Repositories
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {
        private readonly AssetMenagementDbContext _context;
        public LogPatrimonioRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<Log_Patrimonio> Listar()
        {
            return _context.Log_Patrimonio.Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .OrderByDescending(log => log.DataTranferencia)
                .ToList();
        }

        public List<Log_Patrimonio> ObterPorPatrimonio(Guid patrimonioId)
        {
            return _context.Log_Patrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .Where(log => log.PatrimonioId == patrimonioId)
                .OrderByDescending(log => log.DataTranferencia)
                .ToList();
        }
    }
}
