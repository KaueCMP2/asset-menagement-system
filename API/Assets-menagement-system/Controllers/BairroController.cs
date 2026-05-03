using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.BairroDto;
using Assets_menagement_system.DTOs.Cidade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {
        private readonly BairroService _service;
        public BairroController(BairroService service)
        {
            _service = service;
        }

        [HttpGet("/bairros")]
        public ActionResult<List<LerBairroDTO>> Listar()
        {
            try
            {
                List<LerBairroDTO> bairros = _service.Listar();

                return Ok(bairros);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/bairros/{guid}")]
        public ActionResult<LerBairroDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerBairroDTO bairros = _service.ObterPorId(guid);
                return Ok(bairros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/bairros/nome/{nome}")]
        public ActionResult<LerBairroDTO> ObterPorNome(string nome, Guid cidadeId)
        {
            try
            {
                LerBairroDTO bairros = _service.ObterPorNome(nome, cidadeId);
                return Ok(bairros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/bairros")]
        public ActionResult Adicionar(CriarBairroDTO bairroDTO)
        {
            try
            {
                _service.Adicionar(bairroDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/bairros/{guid}")]
        public ActionResult<CriarBairroDTO> Atualizar(CriarBairroDTO bairroDto, Guid guid)
        {
            try
            {
                _service.Atualizar(bairroDto, guid);
                return Ok("Bairro atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}