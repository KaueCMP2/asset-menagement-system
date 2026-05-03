using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.EnderecoDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDTO> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();
            if (enderecos == null)
                throw new Exception("Nenhum endereço encontrado.");

            List<ListarEnderecoDTO> enderecosDTO = enderecos.Select(endereco => new ListarEnderecoDTO
            {
                CEP = endereco.CEP,
                Complemento = endereco.Complemento,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                BairroId = endereco.BairroId
            }).ToList();

            return enderecosDTO;
        }

        public Endereco ObterPorId(Guid guid)
        {
            Endereco endereco = _repository.ObterPorId(guid);
            if (endereco == null)
                throw new Exception("Endereço não encontrado.");
            return endereco;
        }

        public Endereco ObterPorCEP(string cep)
        {
            Endereco endereco = _repository.ObterPorCep(cep);
            if (endereco == null)
                throw new Exception("Endereço não encontrado.");
            return endereco;
        }

        public Endereco ObterPorLogradouro(string nomeBairro, Guid cidadeId)
        {
            Endereco endereco = _repository.ObterPorLogradouro(nomeBairro, cidadeId);
            if(endereco == null)
                throw new Exception("Endereço não encontrado.");

            return endereco;
        }

        public void Adicionar(CriarEnderecoDTO enderecoDTO)
        {
            Endereco endereco = new Endereco
            {
                Logradouro = enderecoDTO.Logradouro,
                Numero = enderecoDTO.Numero,
                Complemento = enderecoDTO.Complemento,
                CEP = enderecoDTO.CEP,
                BairroId = (Guid)enderecoDTO.BairroId
            };
            _repository.Adicionar(endereco);
        }

        public void Atualizar(Guid guid, CriarEnderecoDTO enderecoDTO)
        {
            Endereco endereco = _repository.ObterPorId(guid);
            if (endereco == null)
                throw new Exception("Endereço não encontrado.");

            endereco.Logradouro = enderecoDTO.Logradouro;
            endereco.Numero = enderecoDTO.Numero;
            endereco.Complemento = enderecoDTO.Complemento;
            endereco.CEP = enderecoDTO.CEP;
            endereco.BairroId = (Guid)enderecoDTO.BairroId;

            _repository.Atualizar(endereco, guid);
        }
    }
}
