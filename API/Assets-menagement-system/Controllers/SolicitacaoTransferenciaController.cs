using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.SolicitacaoTransferenciaDTO;
using Assets_menagement_system.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;

        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerSolicitacaoTransferenciaDTO>> Listar()
        {
            List<LerSolicitacaoTransferenciaDTO> solicitacoes = _service.Listar();
            return Ok(solicitacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerSolicitacaoTransferenciaDTO> BuscarPorId(Guid id)
        {
            try
            {
                LerSolicitacaoTransferenciaDTO solicitacao = _service.BuscarPorId(id);
                return Ok(solicitacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarSolicitacaoTransferenciaDTO dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(usuarioIdClaim))
                    return Unauthorized("Usuário não autenticado.");

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(usuarioId, dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("{id}/responder")]
        public ActionResult Responder(Guid id, ResponderSolicitacaoTransferenciaDTO dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim))
                    return Unauthorized("Usuário não autenticado.");

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Responder(id, usuarioId, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
