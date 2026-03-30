using Assets_menagement_system.Application.Services;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.EnderecoDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;
        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarEnderecoDTO>> Listar()
        {
            try
            {
                List<ListarEnderecoDTO> enderecos = _service.Listar();
                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{guid}")]
        public ActionResult<ListarEnderecoDTO> ObterPorId(Guid guid)
        {
            try
            {
                Endereco endereco = _service.ObterPorId(guid);
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CEP/{cep}")]
        public ActionResult<ListarEnderecoDTO> ObterPorCEP(string cep)
        {
            try
            {
                Endereco endereco = _service.ObterPorCEP(cep);
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Logradouro/{logradouro}/cidadeId/{cidadeId}")]
        public ActionResult<ListarEnderecoDTO> ObterPorLogradouro(string cep, Guid cidadeId)
        {
            try
            {
                Endereco endereco = _service.ObterPorLogradouro(cep, cidadeId);
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CriarEnderecoDTO> Adicionar(CriarEnderecoDTO enderecoDTO)
        {
            try
            {
                _service.Adicionar(enderecoDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{guid}")]
        public ActionResult<CriarEnderecoDTO> Atualizar(CriarEnderecoDTO enderecoDTO, Guid guid)
        {
            try
            {
                _service.Atualizar(guid, enderecoDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
