using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly AssetMenagementDbContext _context;
        public StatusTransferenciaRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia.OrderBy(s => s.NomeStatus).ToList();
        }
        public StatusTransferencia ObterPorId(Guid guid)
        {
            return _context.StatusTransferencia.FirstOrDefault(s => s.StatusTransferenciaId == guid);
        }
        public StatusTransferencia ObterPorNome(string nome)
        {
            return _context.StatusTransferencia.FirstOrDefault(s => s.NomeStatus == nome);
        }
    }
}
