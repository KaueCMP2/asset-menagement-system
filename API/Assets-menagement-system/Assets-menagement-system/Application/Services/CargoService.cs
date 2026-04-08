using Assets_menagement_system.Application.Regras;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.CargoDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public List<LerCargoDTO> Listar()
        {
            List<Cargo> cargos = _repository.Listar();

            List<LerCargoDTO> cargosDto = cargos.Select(cargo => new LerCargoDTO
            {
                CargoID = cargo.CargoId,
                NomeCargo = cargo.NomeCargo
            }).ToList();

            return cargosDto;
        }

        public LerCargoDTO BuscarPorId(Guid cargoId)
        {
            Cargo cargo = _repository.BuscarPorId(cargoId);

            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            LerCargoDTO cargoDto = new LerCargoDTO
            {
                CargoID = cargo.CargoId,
                NomeCargo = cargo.NomeCargo
            };

            return cargoDto;
        }

        public void Adicionar(CriarCargoDTO dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoExistente != null)
            {
                throw new DomainException("Já existe um cargo cadastrado com esse nome.");
            }

            Cargo cargo = new Cargo
            {
                NomeCargo = dto.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(Guid cargoId, CriarCargoDTO dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoBanco = _repository.BuscarPorId(cargoId);

            if (cargoBanco == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            Cargo cargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoExistente != null && cargoExistente.CargoId != cargoId)
            {
                throw new DomainException("Já existe um cargo cadastrado com esse nome.");
            }

            cargoBanco.NomeCargo = dto.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }
    }
}
