using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.PatrimonioDTO;
using Assets_menagement_system.DTOs.StatusPatrimonioDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly PatrimonioService _service;
        public PatrimonioController(PatrimonioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<LerPatrimonioDTO>> Listar()
        {
            List<LerPatrimonioDTO> patrimonios = _service.Listar();
            return Ok(patrimonios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<LerPatrimonioDTO> BuscarPorId(Guid id)
        {
            LerPatrimonioDTO patrimonio = _service.BuscarPorId(id);
            if (patrimonio == null)
                return NotFound("Patrimônio não encontrado.");
            return Ok(patrimonio);
        }

        [Authorize(Roles = "Cordenador")]
        [HttpPost("Importar-csv")]
        public ActionResult Adicionar(IFormFile arquivoCsv)
        {
            try
            {
                string usuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrWhiteSpace(usuarioClaim))
                    return Unauthorized("Usuário não encontrado");

                Guid usuarioId = Guid.Parse(usuarioClaim);

                _service.Adicionar(arquivoCsv, usuarioId);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Cordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, AtualizarStatusPatrimonioDTO dto)
        {
            try
            {
                _service.AtualizarStatus(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
