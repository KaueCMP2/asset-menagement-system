using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface ILocalizacaoRepository
    {
        public List<Localizacao> Listar();
        public Localizacao ObterPorId(Guid guid);
        public Localizacao ObterPorNome(string nome);
        public bool AreaExiste(Guid? guid);
        public void Adicionar(Localizacao localizacao);
        public void Atualizar(Localizacao localizacao);
    }
}
