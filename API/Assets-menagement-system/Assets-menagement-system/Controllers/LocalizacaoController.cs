using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.LocalizacaoDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;
        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarLocalizacaoDTO>> Listar()
        {
            List<ListarLocalizacaoDTO> localizacoes = _service.Listar();
            return localizacoes;
        }

        [HttpGet("{guid}")]
        public ActionResult<ListarLocalizacaoDTO> ObterPorId(Guid guid)
        {
            ListarLocalizacaoDTO localizacao = _service.ObterPorId(guid);
            return localizacao;
        }

        [HttpGet("{nome}")]
        public ActionResult<ListarLocalizacaoDTO> ObterPorNome(string nome, Guid guid)
        {
            ListarLocalizacaoDTO localizacao = _service.ObterPorNome(nome, guid);
            return localizacao;
        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDTO localizacaoDTO)
        {
            try
            {
                _service.Adicionar(localizacaoDTO);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{guid}")]
        public ActionResult Atualizar(Guid guid)
        {
            try
            {
                _service.Atualizar(guid);
                return NoContent();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
