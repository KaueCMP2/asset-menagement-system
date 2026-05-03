using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.StatusTransferenciaDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTransferenciaController : ControllerBase
    {
        private readonly StatusTransferenciaService _service;
        public StatusTransferenciaController(StatusTransferenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerStatusTransferenciaDTO>> Listar()
        {
            try
            {
                List<LerStatusTransferenciaDTO> status = _service.Listar();
                return Ok(status);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{guid}")]
        public ActionResult<LerStatusTransferenciaDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerStatusTransferenciaDTO status = _service.ObterPorId(guid);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nome}")]
        public ActionResult<LerStatusTransferenciaDTO> ObterPorNome(string nome)
        {
            try
            {
                LerStatusTransferenciaDTO status = _service.ObterPorNome(nome);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
