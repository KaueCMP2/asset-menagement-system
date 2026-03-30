using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();
        Area ObterPorId(Guid guid);
        Area ObterPorNome(string nome);
        public bool AreaExiste(string nome);
        public void Adicionar(Area area);
        public void Atualizar(Area area);
    }
}
