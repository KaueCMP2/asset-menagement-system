using Assets_menagement_system.Application.Services;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.UsuarioDTO;
using Assets_menagement_system.Exceptions;
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
		public ActionResult<List<ListarUsuarioDto>> Listar()
		{
			List<ListarUsuarioDto> usuarios = _service.Listar();
			return Ok(usuarios);
		}

		[HttpGet("{id}")]
		public ActionResult<ListarUsuarioDto> BuscarPorId(Guid id)
		{
			try
			{
				ListarUsuarioDto usuario = _service.BuscarPorId(id);
				return Ok(usuario);
			}
			catch (DomainException ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public ActionResult Adicionar(CriarUsuarioDto dto)
		{
			try
			{
				_service.Adicionar(dto);
				return Created();
			}
			catch (DomainException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public ActionResult Atualizar(Guid id, CriarUsuarioDto dto)
		{
			try
			{
				_service.Atualizar(id, dto);
				return NoContent();
			}
			catch (DomainException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch("{id}/status")]
		public ActionResult AtualizarStatus(Guid id, AtualizarStatusUsuarioDto dto)
		{
			try
			{
				_service.AtualizarStatus(id, dto);
				return NoContent();
			}
			catch (DomainException ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
    }
}
