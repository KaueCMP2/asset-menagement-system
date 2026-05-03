using Assets_menagement_system.DTOs.TipoAlteracaoDTO;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _tipoAlteracaoRepository;
        public TipoAlteracaoService(ITipoAlteracaoRepository tipoAlteracaoRepository)
        {
            _tipoAlteracaoRepository = tipoAlteracaoRepository;
        }

        public List<LerTipoAlteracaoDTO> Listar()
        {
            var tipos = _tipoAlteracaoRepository.Listar();
            return tipos.Select(t => new LerTipoAlteracaoDTO
            {
                TipoAlteracaoId = t.TipoAlteracaoId,
                NomeTipoAlteracao = t.NomeTipoAlteracao
            }).ToList();
        }
        public LerTipoAlteracaoDTO ObterPorId(Guid guid)
        {
            var tipo = _tipoAlteracaoRepository.ObterPorId(guid);
            if (tipo == null)
                return null;
            return new LerTipoAlteracaoDTO
            {
                TipoAlteracaoId = tipo.TipoAlteracaoId,
                NomeTipoAlteracao = tipo.NomeTipoAlteracao
            };
        }
        public LerTipoAlteracaoDTO ObterPorNome(string nome)
        {
            var tipo = _tipoAlteracaoRepository.ObterPorNome(nome);
            if (tipo == null)
                return null;
            return new LerTipoAlteracaoDTO
            {
                TipoAlteracaoId = tipo.TipoAlteracaoId,
                NomeTipoAlteracao = tipo.NomeTipoAlteracao
            };
        }
        public void Adicionar(CriarTipoAlteracaoDTO criarTipoAlteracaoDTO)
        {
            var tipoAlteracao = new Domains.TipoAlteracao
            {
                NomeTipoAlteracao = criarTipoAlteracaoDTO.NomeTipoAlteracao
            };
            _tipoAlteracaoRepository.Adicionar(tipoAlteracao);
        }
        public void Atualizar(Guid guid, CriarTipoAlteracaoDTO criarTipoAlteracaoDTO)
        {
            var tipoAlteracao = new Domains.TipoAlteracao
            {
                NomeTipoAlteracao = criarTipoAlteracaoDTO.NomeTipoAlteracao
            };
            _tipoAlteracaoRepository.Atualizar(guid, tipoAlteracao);

        }
    }
}