using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.Cidade;
using Assets_menagement_system.DTOs.EnderecoDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;
        public CidadeController(CidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerCidadeDTO>> Listar()
        {
            try
            {
                return Ok(_service.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{guid}")]
        public ActionResult<LerCidadeDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerCidadeDTO cidade = _service.ObterPorId(guid);
                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public ActionResult<LerCidadeDTO> ObterPorNome(string nome)
        {
            try
            {
                LerCidadeDTO cidade = _service.ObterPorNome(nome);
                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCidadeDTO cidadeDTO)
        {
            try
            {
                _service.Adicionar(cidadeDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{guid}")]
        public ActionResult<LerCidadeDTO> Atualizar(CriarCidadeDTO cidadeDTO, Guid guid)
        {
            try
            {
                _service.Atualizar(cidadeDTO, guid);
                return Ok("Cidade atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}