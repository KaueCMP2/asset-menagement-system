using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();
        Area ObterPorId(Guid guid);
    }
}
