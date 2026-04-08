using Assets_menagement_system.Application.Services;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.UsuarioDTO;
using Assets_menagement_system.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerUsuarioDTO>> Listar()
        {
            List<LerUsuarioDTO> usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDTO> BuscarPorId(Guid id)
        {
            try
            {
                LerUsuarioDTO usuario = _service.BuscarPorId(id);
                return Ok(usuario);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public ActionResult Adicionar(CriarUsuarioDTO dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarUsuarioDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPatch("{id}/status")]
        public ActionResult AtualizarStatus(Guid id, AtualizarStatusUsuarioDTO dto)
        {
            try
            {
                _service.AtualizarStatus(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

