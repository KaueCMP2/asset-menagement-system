using Assets_menagement_system.Application.Autenticacao;
using Assets_menagement_system.Application.Regras;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.UsuarioDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Assets_menagement_system.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<LerUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDTO> usuariosDto = usuarios.Select(usuario => new LerUsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                NIF = usuario.NIF,
                NomeUsuario = usuario.NomeUsuario,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraDeTrabalho = usuario.CarteiraDeTrabalho,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario,
                PrimeiroAcesso = (bool)usuario.PrimeiroAcesso,
                EnderecoId = usuario.EnderecoId,
                CargoId = usuario.CargoId,
                TipoUsuarioId = usuario.TipoUsuarioId
            }).ToList();

            return usuariosDto;
        }

        public LerUsuarioDTO BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            LerUsuarioDTO usuarioDto = new LerUsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                NIF = usuario.NIF,
                NomeUsuario = usuario.NomeUsuario,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraDeTrabalho = usuario.CarteiraDeTrabalho,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario,
                PrimeiroAcesso = (bool)usuario.PrimeiroAcesso,
                EnderecoId = usuario.EnderecoId,
                CargoId = usuario.CargoId,
                TipoUsuarioId = usuario.TipoUsuarioId
            };

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDTO dto)
        {
            Validar.ValidarNome(dto.NomeUsuario);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse NIF.");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse CPF.");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse e-mail.");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoId))
            {
                throw new DomainException("Endereço informado não existe.");
            }

            if (!_repository.CargoExiste(dto.CargoId))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioId))
            {
                throw new DomainException("Tipo de usuário informado não existe.");
            }

            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                NomeUsuario = dto.NomeUsuario,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraDeTrabalho = dto.CarteiraDeTrabalho,
                Senha = CriptografarUsuario.CriptografarSenha(dto.NIF),
                Email = dto.Email,
                StatusUsuario = true,
                PrimeiroAcesso = true,
                EnderecoId = dto.EnderecoId,
                CargoId = dto.CargoId,
                TipoUsuarioId = dto.TipoUsuarioId
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid usuarioId, CriarUsuarioDTO dto)
        {
            Validar.ValidarNome(dto.NomeUsuario);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email, usuarioId);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse NIF.");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse CPF.");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse e-mail.");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoId))
            {
                throw new DomainException("Endereço informado não existe.");
            }

            if (!_repository.CargoExiste(dto.CargoId))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioId))
            {
                throw new DomainException("Tipo de usuário informado não existe.");
            }

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.NomeUsuario = dto.NomeUsuario;
            usuarioBanco.RG = dto.RG;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraDeTrabalho = dto.CarteiraDeTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoId = dto.EnderecoId;
            usuarioBanco.CargoId = dto.CargoId;
            usuarioBanco.TipoUsuarioId = dto.TipoUsuarioId;

            _repository.Atualizar(usuarioBanco);
        }

        public void AtualizarStatus(Guid usuarioId, AtualizarStatusUsuarioDTO dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            usuarioBanco.StatusUsuario = dto.StatusUsuario;
            _repository.AtualizarStatus(usuarioBanco);
        }
    }
}
