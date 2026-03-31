using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.TipoPatrimonioDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatrimonioService _service;
        public TipoPatrimonioController(TipoPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerTipoPatrimonioDTO>> Listar()
        {
            try
            {
                List<LerTipoPatrimonioDTO> tipos = _service.Listar();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{guid}")]
        public ActionResult<LerTipoPatrimonioDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerTipoPatrimonioDTO tipo = _service.ObterPorId(guid);
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{nome}")]
        public ActionResult<LerTipoPatrimonioDTO> ObterPorNome(string nome)
        {
            try
            {
                LerTipoPatrimonioDTO tipo = _service.ObterPorNome(nome);
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Adicionar(CriarTipoPatrimonioDTO criarTipoPatrimonioDTO)
        {
            try
            {
                _service.Adicionar(criarTipoPatrimonioDTO);
                return Ok("Tipo de patrimônio adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{guid}")]
        public ActionResult Atualizar(Guid guid, CriarTipoPatrimonioDTO criarTipoPatrimonioDTO)
        {
            try
            {
                _service.Atualizar(guid, criarTipoPatrimonioDTO);
                return Ok("Tipo de patrimônio atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
