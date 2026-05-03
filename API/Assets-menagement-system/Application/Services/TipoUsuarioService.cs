using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.TipoUsuarioDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;
        public TipoUsuarioService(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }
        public List<LerTipoUsuarioDTO> Listar()
        {
            var tipos = _tipoUsuarioRepository.Listar();
            return tipos.Select(t => new LerTipoUsuarioDTO
            {
                TipoUsuarioId = t.TipoUsuarioId,
                Nome = t.Nome
            }).ToList();
        }
        public LerTipoUsuarioDTO ObterPorId(Guid guid)
        {
            var tipo = _tipoUsuarioRepository.ObterPorId(guid);
            if (tipo == null)
                return null;
            return new LerTipoUsuarioDTO
            {
                TipoUsuarioId = tipo.TipoUsuarioId,
                Nome = tipo.Nome
            };
        }
        public LerTipoUsuarioDTO ObterPorNome(string nome)
        {
            var tipo = _tipoUsuarioRepository.ObterPorNome(nome);
            if (tipo == null)
                return null;
            return new LerTipoUsuarioDTO
            {
                TipoUsuarioId = tipo.TipoUsuarioId,
                Nome = tipo.Nome
            };
        }
        public void Adicionar(CriarTipoUsuarioDTO criarTipoUsuarioDTO)
        {
            if (_tipoUsuarioRepository.TipoExiste(null, criarTipoUsuarioDTO.Nome))
                throw new DomainException("Já existe um tipo de usuário com esse nome.");
            TipoUsuario tipoNovo = new TipoUsuario
            {
                Nome = criarTipoUsuarioDTO.Nome
            };
            _tipoUsuarioRepository.Adicionar(tipoNovo);
        }
        public void Atualizar(Guid guid, CriarTipoUsuarioDTO criarTipoUsuarioDTO)
        {
            if (_tipoUsuarioRepository.TipoExiste(guid, criarTipoUsuarioDTO.Nome))
                throw new DomainException("Já existe um tipo de usuário com esse nome.");
            TipoUsuario tipoAtualizado = new TipoUsuario
            {
                TipoUsuarioId = guid,
                Nome = criarTipoUsuarioDTO.Nome
            };
            _tipoUsuarioRepository.Atualizar(guid, tipoAtualizado);
        }
    }
}
