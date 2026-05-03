using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.LogPatrimonioDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPatrimonioController : ControllerBase
    {
        private readonly LogPatrimonioService _service;
        public LogPatrimonioController(LogPatrimonioService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarLogPatrimonioDTO>> Listar()
        {
            List<ListarLogPatrimonioDTO> logs = _service.Listar();
            return Ok(logs);
        }

        [Authorize]
        [HttpGet("patrimonios/{id}")]
        public ActionResult<List<ListarLogPatrimonioDTO>> ListarPorPatrimonio(Guid id)
        {
            List<ListarLogPatrimonioDTO> logs = _service.ListarPorPatrimonioId(id);
            if (logs == null || logs.Count == 0)
                return NotFound("Nenhum log encontrado para o patrimônio especificado.");
            return Ok(logs);
        }
    }
}
