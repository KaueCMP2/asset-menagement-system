using Assets_menagement_system.Application.Services;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.UsuarioDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerUsuarioDTO>> Listar()
        {
            try
            {
                List<LerUsuarioDTO> usuarios = _service.Listar();
                return Ok(usuarios);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
