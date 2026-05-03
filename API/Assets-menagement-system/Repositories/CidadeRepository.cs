using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly AssetMenagementDbContext _ctx;
        public CidadeRepository(AssetMenagementDbContext context)
        {
            _ctx = context;
        }

        public List<Cidade> Listar()
        {
            return _ctx.Cidade.OrderBy(cid => cid.NomeCidade).ToList();
        }

        public Cidade ObterPorId(Guid guid)
        {
            return _ctx.Cidade.FirstOrDefault(c => c.CidadeId == guid);
        }

        public Cidade ObterPorNome(string nome)
        {
            return _ctx.Cidade.FirstOrDefault(c => c.NomeCidade == nome);
        }

        public bool CidadeExiste(string nome)
        {
            return _ctx.Cidade.Any(c => c.NomeCidade == nome);
        }

        public void Adicionar(Cidade cidade)
        {
            _ctx.Cidade.Add(cidade);
            _ctx.SaveChanges();
        }

        public void Atualizar(Cidade cidade, Guid guid)
        {
                Cidade cidadeBanco = _ctx.Cidade.Find(guid);

                cidadeBanco.NomeCidade = cidade.NomeCidade;
                cidadeBanco.NomeEstado = cidade.NomeEstado;

                _ctx.Update(cidadeBanco);
                _ctx.SaveChanges();
        }
    }
}
