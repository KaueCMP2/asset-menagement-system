using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();
        Bairro ObterPorId(Guid guid);
        Bairro ObterPorNome(string nome, Guid cidadeId);
        bool BairroExiste(string nome);
        void Adicionar(Bairro bairro);
        void Atualizar(Bairro bairro, Guid guid);
    }
}
