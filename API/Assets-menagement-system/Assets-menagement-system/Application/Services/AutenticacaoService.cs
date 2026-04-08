using Assets_menagement_system.Application.Autenticacao;
using Assets_menagement_system.Application.Regras;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.AutenticacaoDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Assets_menagement_system.Application.Services
{
    public class AutenticacaoService
    {
        private readonly GeradorTokenJwt _geradorTokenJwt;
        private readonly IUsuarioRepository _repository;
        public AutenticacaoService(GeradorTokenJwt geradorTokenJwt, IUsuarioRepository usuarioRepository)
        {
            _geradorTokenJwt = geradorTokenJwt;
            _repository = usuarioRepository;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitada = CriptografarUsuario.CriptografarSenha(senhaDigitada);

            return hashDigitada.SequenceEqual(senhaHashBanco);
        }
        public TokenDTO Login(LoginDTO loginDTO)
        {

            Usuario usuario = _repository.ObterPorNIFComTipoUsuario(loginDTO.NIF);
            if (usuario == null)
                throw new DomainException("NIF ou senha inválidos ");

            if (usuario.StatusUsuario != true)
                throw new DomainException("Usuário inativo");

            if (!VerificarSenha(loginDTO.Senha, usuario.Senha))
                throw new DomainException("NIF ou senha inválidos");

            string token = _geradorTokenJwt.GerarToken(usuario);

            return new TokenDTO
            {
                Token = token,
                PrimeiroAcesso = (bool)usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.Nome
            };
        }

        public void TrocarPrimeiraSenha(Guid usuarioid, TrocarPrimeiraSenhaDTO dto)
        {
            Validar.ValidarSenha(dto.SenhaAtual);
            Validar.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorId(usuarioid);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado!");

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha))
                throw new DomainException("Senha atual invalida!");

            if (dto.SenhaAtual == dto.NovaSenha)
                throw new DomainException("A nova senha deve ser diferente da senha atual!");

            usuario.Senha = CriptografarUsuario.CriptografarSenha(dto.NovaSenha);
            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }
    }
}
