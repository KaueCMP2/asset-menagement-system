using Assets_menagement_system.DTOs.StatusTransferenciaDTO;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _statusTransferenciaRepository;
        public StatusTransferenciaService(IStatusTransferenciaRepository statusTransferenciaRepository)
        {
            _statusTransferenciaRepository = statusTransferenciaRepository;
        }

        public List<LerStatusTransferenciaDTO> Listar()
        {
            var status = _statusTransferenciaRepository.Listar();
            return status.Select(s => new LerStatusTransferenciaDTO
            {
                StatusTransferenciaId = s.StatusTransferenciaId,
                NomeStatus = s.NomeStatus
            }).ToList();
        }
        public LerStatusTransferenciaDTO ObterPorId(Guid guid)
        {
            var status = _statusTransferenciaRepository.ObterPorId(guid);
            if (status == null)
                return null;
            return new LerStatusTransferenciaDTO
            {
                StatusTransferenciaId = status.StatusTransferenciaId,
                NomeStatus = status.NomeStatus
            };
        }
        public LerStatusTransferenciaDTO ObterPorNome(string nome)
        {
            var status = _statusTransferenciaRepository.ObterPorNome(nome);
            if (status == null)
                return null;
            return new LerStatusTransferenciaDTO
            {
                StatusTransferenciaId = status.StatusTransferenciaId,
                NomeStatus = status.NomeStatus
            };
        }
    }
}

