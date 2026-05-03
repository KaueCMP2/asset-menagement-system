using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.AreaDTO;
using Assets_menagement_system.DTOs.Cidade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _service;
        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarAreaDTO>> Listar()
        {
            try
            {
                List<ListarAreaDTO> areas = _service.Listar();
                return Ok(areas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{guid}")]

        public ActionResult<ListarAreaDTO> ObterPorId(Guid guid)
        {
            try
            {
                ListarAreaDTO area = _service.ObterPorId(guid);
                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nome}")]
        public ActionResult<ListarAreaDTO> ObterPorNome(string nomeArea)
        {
            try
            {
                ListarAreaDTO area = _service.ObterPorNome(nomeArea);
                return Ok(area);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarAreaDTO areaDTO)
        {
            try
            {
                _service.Adicionar(areaDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{guid}")]
        public ActionResult Atualizar(Guid guid, CriarAreaDTO areaDTO)
        {
            try
            {
                _service.Atualizar(guid, areaDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
