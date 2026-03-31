using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.StatusPatrimonioDTO;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;
        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<LerStatusPatrimonioDTO> Listar()
        {
            List<StatusPatrimonio> statusPatrimonios = _repository.Listar();
            List<LerStatusPatrimonioDTO> lerStatusPatrimoniosDTO = statusPatrimonios.Select(st => new LerStatusPatrimonioDTO
            {
                StatusPatrimonioId = st.StatusPatrimonioId,
                NomeStatus = st.NomeStatus,
            }).ToList();
            return lerStatusPatrimoniosDTO;
        }

        public LerStatusPatrimonioDTO ObterPorId(Guid guid)
        {
            StatusPatrimonio statusPatrimonio = _repository.ObterPorId(guid);
            LerStatusPatrimonioDTO lerStatusPatrimonioDTO = new LerStatusPatrimonioDTO
            {
                StatusPatrimonioId = statusPatrimonio.StatusPatrimonioId,
                NomeStatus = statusPatrimonio.NomeStatus,
            };
            return lerStatusPatrimonioDTO;
        }

        public LerStatusPatrimonioDTO ObterPorNome(string nome)
        {
            StatusPatrimonio statusPatrimonio = _repository.ObterPorNome(nome);
            LerStatusPatrimonioDTO lerStatusPatrimonioDTO = new LerStatusPatrimonioDTO
            {
                StatusPatrimonioId = statusPatrimonio.StatusPatrimonioId,
                NomeStatus = statusPatrimonio.NomeStatus,
            };
            return lerStatusPatrimonioDTO;
        }

        public void Adicionar(CriarStatusPatrimonioDTO criarStatusPatrimonioDTO)
        {
            StatusPatrimonio statusNovo = new StatusPatrimonio
            {
                NomeStatus = criarStatusPatrimonioDTO.NomeStatus
            };
            _repository.Adicionar(statusNovo);
        }

        public void Atualizar(Guid guid, CriarStatusPatrimonioDTO criarStatusPatrimonioDTO)
        {
            StatusPatrimonio statusAtualizado = new StatusPatrimonio
            {
                NomeStatus = criarStatusPatrimonioDTO.NomeStatus
            };
            _repository.Atualizar(guid, statusAtualizado);
        }
    }
}
