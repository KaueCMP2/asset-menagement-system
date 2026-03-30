using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;
using System.Runtime.CompilerServices;

namespace Assets_menagement_system.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AssetMenagementDbContext _context;
        public EnderecoRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(e => e.Logradouro).ToList();
        }

        public Endereco ObterPorId(Guid guid)
        {
            return _context.Endereco.Find(guid);
        }

        public Endereco ObterPorCep(string cep)
        {
            return _context.Endereco.FirstOrDefault(e => e.CEP == cep);
        }

        public bool EnderecoExiste(string logradouro)
        {
            return _context.Endereco.Any(e => e.Logradouro == logradouro);
        }   

        public Endereco ObterPorLogradouro(string logradouro, Guid guid)
        {
            return _context.Endereco.FirstOrDefault(e => e.Logradouro == logradouro);
        }

        public bool EnderecoExiste(string logradouro, Guid cidadeId)
        {
            return _context.Endereco.Any(e => e.Logradouro == logradouro);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco, Guid guid)
        {
            if (endereco == null)
                return;
            Endereco enderecoBanco = _context.Endereco.FirstOrDefault(e => e.EnderecoId == guid);

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;
            enderecoBanco.BairroId = endereco.BairroId;

            _context.Endereco.Update(enderecoBanco);
            _context.SaveChanges();
        }
    }
}