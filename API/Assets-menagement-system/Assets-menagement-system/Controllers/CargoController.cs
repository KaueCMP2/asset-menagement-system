using Assets_menagement_system.Application.Services;
using Assets_menagement_system.DTOs.CargoDTO;
using Assets_menagement_system.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerCargoDTO>> Listar()
        {
            List<LerCargoDTO> cargos = _service.Listar();
            return Ok(cargos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerCargoDTO> BuscarPorId(Guid id)
        {
            try
            {
                LerCargoDTO cargo = _service.BuscarPorId(id);
                return Ok(cargo);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public ActionResult Adicionar(CriarCargoDTO dto)
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
        [Authorize(Roles = "Coordenador")]
        public ActionResult Atualizar(Guid id, CriarCargoDTO dto)
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
    }
}
