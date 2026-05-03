using Assets_menagement_system.Application.Services;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.TipoAlteracaoDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {
        private readonly TipoAlteracaoService _service;
        [HttpGet]
        public ActionResult<List<LerTipoAlteracaoDTO>> Listar()
        {
            try
            {
                List<LerTipoAlteracaoDTO> tipoAlteracoesDTO = _service.Listar();
                return Ok(tipoAlteracoesDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{guid}")]
        public ActionResult<LerTipoAlteracaoDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerTipoAlteracaoDTO tipoAlteracaoDTO = _service.ObterPorId(guid);
                return Ok(tipoAlteracaoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{nome}")]
        public ActionResult<LerTipoAlteracaoDTO> ObterPorNome(string nome)
        {
            try
            {
                LerTipoAlteracaoDTO tipoAlteracaoDTO = _service.ObterPorNome(nome);
                return Ok(tipoAlteracaoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<CriarTipoAlteracaoDTO> Adicionar(CriarTipoAlteracaoDTO criarTipoAlteracaoDTO)
        {
            try
            {
                _service.Adicionar(criarTipoAlteracaoDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{guid}")]
        public ActionResult Atualizar(Guid guid, CriarTipoAlteracaoDTO atualizarTipoAlteracaoDTO)
        {
            try
            {
                _service.Atualizar(guid, atualizarTipoAlteracaoDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
