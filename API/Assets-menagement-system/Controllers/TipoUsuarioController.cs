using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.TipoUsuarioDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly TipoUsuarioService _service;
        public TipoUsuarioController(TipoUsuarioService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<LerTipoUsuarioDTO>> Listar()
        {
            try
            {
                List<LerTipoUsuarioDTO> tipos = _service.Listar();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{guid}")]
        public ActionResult<LerTipoUsuarioDTO> ObterPorId(Guid guid)
        {
            try
            {
                LerTipoUsuarioDTO tipo = _service.ObterPorId(guid);
                if (tipo == null)
                    return NotFound("Tipo de usuário não encontrado.");
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{nome}")]
        public ActionResult<LerTipoUsuarioDTO> ObterPorId(string nome)
        {
            try
            {
                LerTipoUsuarioDTO tipo = _service.ObterPorNome(nome);
                if (tipo == null)
                    return NotFound("Tipo de usuário não encontrado.");
                return Ok(tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Adicionar(CriarTipoUsuarioDTO criarTipoUsuarioDTO)
        {
            try
            {
                _service.Adicionar(criarTipoUsuarioDTO);
                return Ok("Tipo de usuário adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{guid}")]
        public ActionResult Atualizar(Guid guid, CriarTipoUsuarioDTO criarTipoUsuarioDTO)
        {
            try
            {
                _service.Atualizar(guid, criarTipoUsuarioDTO);
                return Ok("Tipo de usuário atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
