using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.LogPatrimonioDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class LogPatrimonioService
    {
        private readonly ILogPatrimonioRepository _repository;
        public LogPatrimonioService(ILogPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLogPatrimonioDTO> Listar()
        {
            List<Log_Patrimonio> logs = _repository.Listar();

            List<ListarLogPatrimonioDTO> logsDto = logs.Select(log => new
            ListarLogPatrimonioDTO
            {
                LogPatrimonioId = log.LogPatrimonioId,
                DataTransferencia = log.DataTranferencia,
                PatrimonoId = log.PatrimonioId,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipoAlteracao,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.NomeUsuario,
                Localizacao = log.Localizacao.NomeLocal
            }).ToList();

            return logsDto;
        }

        public List<ListarLogPatrimonioDTO> ListarPorPatrimonioId(Guid patrimonioId)
        {
            List<Log_Patrimonio> logs = _repository.ObterPorPatrimonio(patrimonioId);
            if (logs == null)
                throw new DomainException("Patrimonio não encontrado");

            List<ListarLogPatrimonioDTO> logsDto = logs.Select(log => new
            ListarLogPatrimonioDTO
            {
                LogPatrimonioId = log.LogPatrimonioId,
                DataTransferencia = log.DataTranferencia,
                PatrimonoId = log.PatrimonioId,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao,
                TipoAlteracao = log.TipoAlteracao.NomeTipoAlteracao,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                Usuario = log.Usuario.NomeUsuario,
                Localizacao = log.Localizacao.NomeLocal
            }).ToList();

            return logsDto;
        }
    }
}
