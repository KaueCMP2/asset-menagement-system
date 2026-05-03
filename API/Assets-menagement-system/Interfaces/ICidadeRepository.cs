using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.AreaDTO;

namespace Assets_menagement_system.Interfaces
{
    public interface ICidadeRepository
    {
        List<Cidade> Listar();
        Cidade ObterPorId(Guid guid);
        Cidade ObterPorNome(string nome);
        public bool CidadeExiste(string nome);
        public void Adicionar(Cidade cidade);
        public void Atualizar(Cidade cidade, Guid guid);

    }
}
