using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.Cidade;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;
        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<LerCidadeDTO> Listar()
        {
            List<Cidade> cidades = _repository.Listar();
            List<LerCidadeDTO> cidadesDTO = cidades.Select(cidades => new LerCidadeDTO
            {
                CidadeId = cidades.CidadeId,
                NomeCidade = cidades.NomeCidade,
                NomeEstado = cidades.NomeEstado
            }).ToList();
            return cidadesDTO;
        }

        public LerCidadeDTO ObterPorId(Guid guid)
        {
            Cidade cidade = _repository.ObterPorId(guid);
            if (cidade == null)
                throw new DomainException("Cidade nao encontrada!");

            LerCidadeDTO cidadeDTO = new LerCidadeDTO
            {
                CidadeId = cidade.CidadeId,
                NomeCidade = cidade.NomeCidade,
                NomeEstado = cidade.NomeEstado
            };
            return cidadeDTO;
        }

        public LerCidadeDTO ObterPorNome(string nome)
        {
            Cidade cidade = _repository.ObterPorNome(nome);
            if (cidade == null)
                throw new DomainException("Cidade nao encontrada!");

            LerCidadeDTO cidadeDTO = new LerCidadeDTO
            {
                CidadeId = cidade.CidadeId,
                NomeCidade = cidade.NomeCidade,
                NomeEstado = cidade.NomeEstado
            };
            return cidadeDTO;
        }

        public void Adicionar(CriarCidadeDTO cidadeDTO)
        {
            if (_repository.CidadeExiste(cidadeDTO.NomeCidade))
                throw new Exception("Cidade já existe");
            Cidade cidade = new Cidade
            {
                CidadeId = Guid.NewGuid(),
                NomeCidade = cidadeDTO.NomeCidade,
                NomeEstado = cidadeDTO.NomeEstado
            };
            _repository.Adicionar(cidade);
        }

        public void Atualizar(CriarCidadeDTO cidadeDTO, Guid guid)
        {
            
            Cidade cidade = _repository.ObterPorId(guid);
            if (cidade == null)
                throw new Exception("Cidade não encontrada");

            cidade.NomeCidade = cidadeDTO.NomeCidade;
            cidade.NomeEstado = cidadeDTO.NomeEstado;

            _repository.Atualizar(cidade, guid);
        }
    }
}
