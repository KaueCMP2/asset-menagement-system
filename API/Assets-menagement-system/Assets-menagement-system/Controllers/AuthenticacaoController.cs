using Assets_menagement_system.Application.Authenticacao;
using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.AuthenticacaoDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticacaoController : ControllerBase
    {
        private readonly AuthenticacaoService _service;
        public AuthenticacaoController(AuthenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult<TokenDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                TokenDTO token = _service.Login(loginDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("trocar-senha")]
        public ActionResult TrocarPrimeiraSenha(TrocarPrimeiraSenhaDTO dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (usuarioIdClaim == null)
                    return Unauthorized("Usuário não autenticado.");

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.TrocarPrimeiraSenha(usuarioId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
