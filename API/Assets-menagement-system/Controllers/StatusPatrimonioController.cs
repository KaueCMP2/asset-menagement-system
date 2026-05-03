using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.StatusPatrimonioDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;
        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerStatusPatrimonioDTO>> Listar()
        {
            try
            {
                List<LerStatusPatrimonioDTO> status = _service.Listar();
                return Ok(status);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{guid}")]
        public ActionResult<LerStatusPatrimonioDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerStatusPatrimonioDTO status = _service.ObterPorId(guid);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nome}")]
        public ActionResult<LerStatusPatrimonioDTO> ObterPorNome(string nome)
        {
            try
            {
                LerStatusPatrimonioDTO status = _service.ObterPorNome(nome);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Adicionar(CriarStatusPatrimonioDTO criarStatusPatrimonioDTO)
        {
            try
            {
                _service.Adicionar(criarStatusPatrimonioDTO);
                return Ok("Status de patrimônio adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{guid}")]
        public ActionResult Atualizar(Guid guid, CriarStatusPatrimonioDTO criarStatusPatrimonioDTO)
        {
            try
            {
                _service.Atualizar(guid, criarStatusPatrimonioDTO);
                return Ok("Status de patrimônio atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
