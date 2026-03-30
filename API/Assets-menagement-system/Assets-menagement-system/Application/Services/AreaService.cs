using Assets_menagement_system.Application.Regras;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.AreaDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class AreaService
    {
        private readonly IAreaRepository _repository;
        public AreaService(IAreaRepository repository) => _repository = repository;

        private static ListarAreaDTO ListarDTO(Area area)
        {
            ListarAreaDTO lerDTO = new ListarAreaDTO()
            {
                AreaId = area.AreaId,
                NomeArea = area.NomeArea
            };

            return lerDTO;
        }

        public List<ListarAreaDTO> Listar()
        {
            List<Area> list = _repository.Listar();
            List<ListarAreaDTO> listarDTO = list.Select(listagem => new ListarAreaDTO
            {
                AreaId = listagem.AreaId,
                NomeArea = listagem.NomeArea
            }).ToList();

            return listarDTO;
        }



        public ListarAreaDTO ObterPorId(Guid guid)
        {
            Area area = _repository.ObterPorId(guid);
            if (area == null)
                throw new DomainException("Area não existe");

            ListarAreaDTO areaDto = new ListarAreaDTO
            {
                AreaId = area.AreaId,
                NomeArea = area.NomeArea
            };

            return areaDto;
        }


        public ListarAreaDTO ObterPorNome(string nomeArea)
        {
            Area area = _repository.ObterPorNome(nomeArea);
            if (area == null)
                throw new DomainException("Nome Inválido");

            ListarAreaDTO areaDTO = new ListarAreaDTO
            {
                NomeArea = area.NomeArea,
                AreaId = area.AreaId
            };
            return areaDTO;
        }


        public void Adicionar(CriarAreaDTO areaDTO)
        {
            ValidarCriacaoDTO.ValidarNome(areaDTO.NomeArea);

            if (_repository.AreaExiste(areaDTO.NomeArea) == true)
                throw new DomainException("Já existe uma área com este nome");
            Area area = new Area
            {
                NomeArea = areaDTO.NomeArea
            };
            _repository.Adicionar(area);
        }

        public void Atualizar(Guid guid, CriarAreaDTO areaDTO)
        {
            ValidarCriacaoDTO.ValidarNome(areaDTO.NomeArea);
            Area areaBanco = _repository.ObterPorId(guid);
            Area areaExistente = _repository.ObterPorNome(areaDTO.NomeArea);

            if (areaBanco == null)
                throw new DomainException("Esta área não foi encontrada");

            if (areaExistente != null)
                throw new DomainException("Já existe uma área cadastrada com este nome");

            areaBanco.NomeArea = areaDTO.NomeArea;

            _repository.Atualizar(areaBanco);
        }
    }
}
