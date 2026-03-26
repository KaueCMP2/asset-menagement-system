using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.LocalizacaoDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;
using Assets_menagement_system.Repositories;

namespace Assets_menagement_system.Application.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;
        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizacaoDTO> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();
            List<ListarLocalizacaoDTO> localizacoesDTO = localizacoes.Select(loc => new ListarLocalizacaoDTO
            {
                LocalizacaoId = loc.LocalizacaoId,
                NomeLocal = loc.NomeLocal,
                LocalSAP = loc.LocalSAP,
                DescricaoSAP = loc.DescricaoSAP,
                AreaId = loc.LocalizacaoId
            }).ToList();
            return localizacoesDTO;
        }

        public ListarLocalizacaoDTO ObterPorId(Guid guid)
        {
            Localizacao localizacoes = _repository.ObterPorId(guid);
            ListarLocalizacaoDTO localizacoesDTO = new ListarLocalizacaoDTO
            {
                LocalizacaoId = localizacoes.LocalizacaoId,
                NomeLocal = localizacoes.NomeLocal,
                LocalSAP = localizacoes.LocalSAP,
                DescricaoSAP = localizacoes.DescricaoSAP,
                AreaId = localizacoes.LocalizacaoId
            };
            return localizacoesDTO;
        }

        public ListarLocalizacaoDTO ObterPorNome(string nome)
        {
            Localizacao localizacoes = _repository.ObterPorNome(nome);
            ListarLocalizacaoDTO localizacoesDTO = new ListarLocalizacaoDTO
            {
                LocalizacaoId = localizacoes.LocalizacaoId,
                NomeLocal = localizacoes.NomeLocal,
                LocalSAP = localizacoes.LocalSAP,
                DescricaoSAP = localizacoes.DescricaoSAP,
                AreaId = localizacoes.LocalizacaoId
            };
            return localizacoesDTO;
        }

        public void Adicionar(CriarLocalizacaoDTO localizacaoDTO)
        {
            Localizacao localizacao = _repository.ObterPorNome(localizacaoDTO.NomeLocal);
            if (localizacao != null)
                throw new DomainException("Já existe uma Localização com esse nome!");

            if (_repository.AreaExiste(localizacaoDTO.AreaId) == false)
                throw new DomainException("Area não encontrada!");

            Localizacao localizacaoCriada = new Localizacao
            {
                NomeLocal = localizacaoDTO.NomeLocal,
                LocalSAP = localizacaoDTO.LocalSAP,
                DescricaoSAP = localizacaoDTO.DescricaoSAP,
                AreaId = localizacaoDTO.AreaId
            };
            _repository.Adicionar(localizacaoCriada);
        }

        public void Atualizar(Guid guid)
        {
            Localizacao localizacao = _repository.ObterPorId(guid);
            if (localizacao != null)
                throw new DomainException("Já existe uma Localização com esse nome!");

            Localizacao localizacaoCriada = new Localizacao
            {
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaId = localizacao.AreaId
            };
            _repository.Atualizar(localizacaoCriada);
        }
    }
}
