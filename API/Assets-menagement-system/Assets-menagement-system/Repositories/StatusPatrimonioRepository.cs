using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly AssetMenagementDbContext _context;
        public StatusPatrimonioRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.OrderBy(s => s.NomeStatus).ToList();
        }
        public StatusPatrimonio ObterPorId(Guid guid)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.StatusPatrimonioId == guid);
        }
        public StatusPatrimonio ObterPorNome(string nome)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.NomeStatus == nome);
        }
        public bool StatusExiste(Guid? guid = null, string? nome = null)
        {
            return _context.StatusPatrimonio.Any(s => s.StatusPatrimonioId == guid || s.NomeStatus == nome);
        }
        public void Adicionar(StatusPatrimonio status)
        {
            StatusPatrimonio statusNovo = new StatusPatrimonio
            {
                NomeStatus = status.NomeStatus
            };
            _context.Add(statusNovo);
            _context.SaveChanges();
        }
        public void Atualizar(Guid guid, StatusPatrimonio status)
        {
            StatusPatrimonio statusBanco = _context.StatusPatrimonio.Find(guid);
            if (statusBanco == null)
                return;
            statusBanco.NomeStatus = status.NomeStatus;
            _context.Update(statusBanco);
            _context.SaveChanges();
        }
    }
}