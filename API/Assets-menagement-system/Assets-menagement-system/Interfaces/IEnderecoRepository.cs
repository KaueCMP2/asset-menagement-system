using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.AreaDTO;

namespace Assets_menagement_system.Interfaces
{
    public interface IEnderecoRepository
    {
        public List<Endereco> Listar();
        public Endereco ObterPorId(Guid guid);
        public Endereco ObterPorCep(string cep);
        public Endereco ObterPorLogradouro(string nomeBairro, Guid cidadeId);
        public bool EnderecoExiste(string logradouro);
        public void Adicionar(Endereco endereco);
        public void Atualizar(Endereco endereco, Guid guid);
    }
}
