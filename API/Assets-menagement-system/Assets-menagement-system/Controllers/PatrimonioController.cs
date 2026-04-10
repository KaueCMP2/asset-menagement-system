using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.PatrimonioDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarPatrimonioDTO dto)
        {
            _service.Adicionar(dto);
            return Created();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, AtualizarPatrimonioDTO dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
