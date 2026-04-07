using Assets_menagement_system.DTOs.UsuarioDTO;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<LerUsuarioDTO> Listar()
        {
            var usuarios = _usuarioRepository.Listar();
            return usuarios.Select(u => new LerUsuarioDTO
            {
                UsuarioId = u.UsuarioId,
                NomeUsuario = u.NomeUsuario,
                Email = u.Email,
                CPF = u.CPF,
                RG = u.RG,
                NIF = u.NIF,
                CarteiraDeTabalho = u.CarteiraDeTabalho,
                StatusUsuario = u.StatusUsuario,
                EnderecoId = u.EnderecoId,
                PrimeiroAcesso = (bool)u.PrimeiroAcesso
            }).ToList();
        }
    }
}
