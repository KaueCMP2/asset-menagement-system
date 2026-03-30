using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.BairroDto;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class BairroService
    {
        private readonly IBairroRepository _repository;
        public BairroService(IBairroRepository repository)
        {
            _repository = repository;
        }

        public List<LerBairroDTO> Listar()
        {
            List<Bairro> bairros = _repository.Listar();
            List<LerBairroDTO> bairrosDTO = bairros.Select(bairro => new LerBairroDTO
            {
                BairroId = bairro.BairroId,
                NomeBairro = bairro.NomeBairro,
                CidadeId = bairro.CidadeId
            }).ToList();
            return bairrosDTO;
        }
        public LerBairroDTO ObterPorId(Guid guid)
        {
            Bairro bairro = _repository.ObterPorId(guid);
            LerBairroDTO bairroBancoDto = new LerBairroDTO
            {
                BairroId = bairro.BairroId,
                NomeBairro = bairro.NomeBairro,
                CidadeId = bairro.CidadeId
            };
            return bairroBancoDto;
        }

        public LerBairroDTO ObterPorNome(string nome, Guid cidadeId)
        {
            Bairro bairro = _repository.ObterPorNome(nome, cidadeId);
            if (bairro == null)
                throw new DomainException("Bairro não encontrado!");
            LerBairroDTO bairroDTO = new LerBairroDTO
            {
                BairroId = bairro.BairroId,
                NomeBairro = bairro.NomeBairro,
                CidadeId = bairro.CidadeId
            };
            return bairroDTO;
        }
        public void Adicionar(CriarBairroDTO bairroDTO)
        {
            if (_repository.BairroExiste(bairroDTO.NomeBairro))
                throw new Exception("Bairro já existe");
            Bairro bairro = new Bairro
            {
                NomeBairro = bairroDTO.NomeBairro,
                CidadeId = (Guid)bairroDTO.CidadeId
            };
            _repository.Adicionar(bairro);
        }
        public void Atualizar(CriarBairroDTO bairroDTO, Guid guid)
        {
            string nome = _repository.ObterPorId(guid).NomeBairro;
            if (!_repository.BairroExiste(nome))
                throw new DomainException("Bairro não encontrado");

            Bairro bairro = new Bairro
            {
                NomeBairro = bairroDTO.NomeBairro,
                CidadeId = (Guid)bairroDTO.CidadeId
            };
            _repository.Atualizar(bairro, guid);
        }
    }
}

