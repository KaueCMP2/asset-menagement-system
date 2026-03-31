using Assets_menagement_system.DTOs.TipoPatrimonioDTO;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;
        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<LerTipoPatrimonioDTO> Listar()
        {
            var tipos = _repository.Listar();
            var tiposDTO = new List<LerTipoPatrimonioDTO>();
            foreach (var tipo in tipos)
            {
                tiposDTO.Add(new LerTipoPatrimonioDTO
                {
                    TipoPatrimonioId = tipo.TipoPatrimonioId,
                    NomeTipo = tipo.NomeTipo
                });
            }
            return tiposDTO;
        }
        public LerTipoPatrimonioDTO ObterPorId(Guid guid)
        {
            var tipo = _repository.ObterPorId(guid);
            if (tipo == null)
                return null;
            return new LerTipoPatrimonioDTO
            {
                TipoPatrimonioId = tipo.TipoPatrimonioId,
                NomeTipo = tipo.NomeTipo
            };
        }
        public LerTipoPatrimonioDTO ObterPorNome(string nome)
        {
            var tipo = _repository.ObterPorNome(nome);
            if (tipo == null)
                return null;
            return new LerTipoPatrimonioDTO
            {
                TipoPatrimonioId = tipo.TipoPatrimonioId,
                NomeTipo = tipo.NomeTipo
            };
        }
        public void Adicionar(CriarTipoPatrimonioDTO criarTipoPatrimonioDTO)
        {
            var tipo = new Domains.TipoPatrimonio
            {
                NomeTipo = criarTipoPatrimonioDTO.NomeTipo
            };
            _repository.Adicionar(tipo);
        }
        public void Atualizar(Guid guid, CriarTipoPatrimonioDTO criarTipoPatrimonioDTO)
        {
            var tipo = new Domains.TipoPatrimonio
            {
                NomeTipo = criarTipoPatrimonioDTO.NomeTipo
            };
            _repository.Atualizar(guid, tipo);
        }
    }
}
