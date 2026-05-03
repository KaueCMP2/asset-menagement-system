using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;
using Microsoft.Identity.Client;

namespace Assets_menagement_system.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private AssetMenagementDbContext _context;
        public BairroRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }
        public List<Bairro> Listar()
        {
            List<Bairro> bairros = _context.Bairro.OrderBy(b => b.NomeBairro).ToList();
            return bairros;
        }
        public Bairro ObterPorId(Guid guid)
        {
            return _context.Bairro.Find(guid);
        }
        public Bairro ObterPorNome(string nome, Guid cidadeId)
        {
            return _context.Bairro.FirstOrDefault(b => b.NomeBairro == nome && b.CidadeId == cidadeId);
        }
        public bool BairroExiste(string nome)
        {
            return _context.Bairro.Any(b => b.NomeBairro == nome);
        }
        public void Adicionar(Bairro bairro)
        {
            Bairro bairroBanco = new Bairro
            {
                NomeBairro = bairro.NomeBairro,
                CidadeId = bairro.CidadeId,
            };
            _context.Add(bairroBanco);
            _context.SaveChanges();
        }
        public void Atualizar(Bairro bairro, Guid guid)
        {
            Bairro bairroBanco = _context.Bairro.Find(guid);

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.CidadeId = bairro.CidadeId;

            _context.Update(bairroBanco);
            _context.SaveChanges();
        }
    }
}
