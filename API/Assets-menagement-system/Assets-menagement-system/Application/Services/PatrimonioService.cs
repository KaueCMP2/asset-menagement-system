using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.PatrimonioDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets_menagement_system.Application.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;
        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<LerPatrimonioDTO> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();
            return patrimonios.Select(p => new LerPatrimonioDTO
            {
                PatrimonioId = p.PatrimonioId,
                Denominacao = p.Denominacao,
                NumeroSerie = p.NumeroSerie,
                Valor = p.Valor,
                Imagem = p.Imagem,
                LocalizacaoId = p.LocalizacaoId,
                StatusPatrimonioId = p.StatusPatrimonioId
            }).ToList();
        }

        public LerPatrimonioDTO BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);
            if (patrimonio == null)
                return null;

            return new LerPatrimonioDTO
            {
                PatrimonioId = patrimonio.PatrimonioId,
                Denominacao = patrimonio.Denominacao,
                NumeroSerie = patrimonio.NumeroSerie,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoId = patrimonio.LocalizacaoId,
                StatusPatrimonioId = patrimonio.StatusPatrimonioId
            };
        }

        public void Adicionar(CriarPatrimonioDTO patrimonioDTO)
        {
            Patrimonio patrimonio = new Patrimonio
            {
                PatrimonioId = Guid.NewGuid(),
                Denominacao = patrimonioDTO.Denominacao,
                NumeroSerie = patrimonioDTO.NumeroSerie,
                Valor = patrimonioDTO.Valor,
                Imagem = patrimonioDTO.Imagem,
                LocalizacaoId = patrimonioDTO.LocalizacaoId,
                StatusPatrimonioId = patrimonioDTO.StatusPatrimonioId
            };

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Criação");
            if (tipoAlteracao == null)
                throw new DomainException("Tipo de alteração 'Criação' não encontrado.");

            Log_Patrimonio log = new Log_Patrimonio
            {
                LogPatrimonioId = Guid.NewGuid(),
                PatrimonioId = patrimonio.PatrimonioId,
                DataTranferencia = DateTime.UtcNow,
                TipoAlteracaoId = tipoAlteracao.TipoAlteracaoId,
                UsuarioId = Guid.Empty // Substitua pelo ID do usuário autenticado, se disponível
            };

            _repository.Adicionar(patrimonio);
            _repository.AdicionarLog(log);
        }
        public void Atualizar(Guid patrimonioId, AtualizarPatrimonioDTO patrimonioDTO)
        {
            Patrimonio patrimonio = new Patrimonio
            {
                PatrimonioId = patrimonioId,
                Denominacao = patrimonioDTO.Denominacao,
                Valor = patrimonioDTO.Valor,
                Imagem = patrimonioDTO.Imagem,
                LocalizacaoId = patrimonioDTO.LocalizacaoId,
                StatusPatrimonioId = patrimonioDTO.StatusPatrimonioId
            };

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Atualização");
            if (tipoAlteracao == null)
                throw new DomainException("Tipo de alteração 'Atualização' não encontrado.");

            Log_Patrimonio log = new Log_Patrimonio
            {
                LogPatrimonioId = Guid.NewGuid(),
                PatrimonioId = patrimonio.PatrimonioId,
                DataTranferencia = DateTime.UtcNow,
                TipoAlteracaoId = tipoAlteracao.TipoAlteracaoId,
                UsuarioId = Guid.Empty // Substitua pelo ID do usuário autenticado, se disponível
            };

            _repository.AdicionarLog(log);
            _repository.Atualizar(patrimonio);
        }

        public void AtualizarStatus(Guid patrimonioId, Guid statusPatrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);
            if (patrimonio == null)
                throw new DomainException("Patrimônio não encontrado.");

            TipoAlteracao tipo = _repository.BuscarTipoAlteracaoPorNome("Atualização de Status");
            if (tipo == null)
                throw new DomainException("Tipo de alteração 'Atualização de Status' não encontrado.");

            StatusPatrimonio statusPatrimonio = _repository.BuscarStatusPatrimonioPorId(statusPatrimonioId);
            if (statusPatrimonio == null)
                throw new DomainException("Status do patrimônio não encontrado.");

            patrimonio.StatusPatrimonioId = statusPatrimonioId;

            Log_Patrimonio log = new Log_Patrimonio
            {
                TipoAlteracaoId = tipo.TipoAlteracaoId,
                StatusPatrimonioId = statusPatrimonio.StatusPatrimonioId
            };

            _repository.AdicionarLog(log);
            _repository.AtualizarStatus(patrimonio);
        }
    }
}
