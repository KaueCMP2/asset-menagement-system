using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assets_menagement_system.Repositories
{
    public class UsuarioRepostory : IUsuarioRepository
    {
        private readonly AssetMenagementDbContext _context;

        public UsuarioRepostory(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(usuario => usuario.NomeUsuario).ToList();
        }

        public Usuario BuscarPorId(Guid usuarioId)
        {
            return _context.Usuario.Find(usuarioId);
        }

        public Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioId = null)
        {
            var consulta = _context.Usuario.AsQueryable();

            if (usuarioId.HasValue)
            {
                consulta = consulta.Where(usuario => usuario.UsuarioId != usuarioId.Value);
            }

            return consulta.FirstOrDefault(usuario =>
                usuario.NIF == nif ||
                usuario.CPF == cpf ||
                usuario.Email.ToLower() == email.ToLower()
                );
        }

        public bool EnderecoExiste(Guid enderecoId)
        {
            return _context.Endereco.Any(endereco => endereco.EnderecoId == enderecoId);
        }

        public bool CargoExiste(Guid cargoId)
        {
            return _context.Cargo.Any(cargo => cargo.CargoId == cargoId);
        }

        public bool TipoUsuarioExiste(Guid tipoUsuarioId)
        {
            return _context.TipoUsuario.Any(tipoUsuario => tipoUsuario.TipoUsuarioId == tipoUsuarioId);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioId);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.NomeUsuario = usuario.NomeUsuario;
            usuarioBanco.RG = usuario.RG;
            usuarioBanco.CPF = usuario.CPF;
            usuarioBanco.CarteiraDeTabalho = usuario.CarteiraDeTabalho;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.EnderecoId = usuario.EnderecoId;
            usuarioBanco.CargoId = usuario.CargoId;
            usuarioBanco.TipoUsuarioId = usuario.TipoUsuarioId;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioId);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.StatusUsuario = usuario.StatusUsuario;
            _context.SaveChanges();
        }

        public Usuario ObterPorNIFComTipoUsuario(string nif)
        {
            return _context.Usuario.Include(usuario => usuario.TipoUsuario)
                .FirstOrDefault(usuario => usuario.NIF == nif);
        }

        public void AtualizarSenha(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioId);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();
        }

        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioId);

            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.PrimeiroAcesso = usuario.PrimeiroAcesso;
            _context.SaveChanges();
        }
    }
}
