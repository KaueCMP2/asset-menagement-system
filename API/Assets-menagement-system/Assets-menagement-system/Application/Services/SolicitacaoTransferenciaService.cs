using Assets_menagement_system.Application.Regras;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.SolicitacaoTransferenciaDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;

namespace Assets_menagement_system.Application.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<LerSolicitacaoTransferenciaDTO> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();

            List<LerSolicitacaoTransferenciaDTO> solicitacoesDto = solicitacoes.Select(solicitacao => new LerSolicitacaoTransferenciaDTO
            {
                SolicitacaoId = solicitacao.SolicitacaoId,
                DataSolicitacao = solicitacao.DataSolicitacao,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaId = solicitacao.StatusTransferenciaId,
                UsuarioSolicitacaoId = solicitacao.UsuarioSolicitacaoId,
                UsuarioAprovacaoId = solicitacao.UsuarioAprovacaoId,
                PatrimonioId = solicitacao.PatrimonioId,
                LocalizacaoId = solicitacao.LocalizacaoId
            }).ToList();

            return solicitacoesDto;
        }

        public LerSolicitacaoTransferenciaDTO BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            LerSolicitacaoTransferenciaDTO solicitacaoDto = new LerSolicitacaoTransferenciaDTO
            {
                SolicitacaoId = solicitacao.SolicitacaoId,
                DataSolicitacao = solicitacao.DataSolicitacao,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaId = solicitacao.StatusTransferenciaId,
                UsuarioSolicitacaoId = solicitacao.UsuarioSolicitacaoId,
                UsuarioAprovacaoId = solicitacao.UsuarioAprovacaoId,
                PatrimonioId = solicitacao.PatrimonioId,
                LocalizacaoId = solicitacao.LocalizacaoId
            };

            return solicitacaoDto;
        }

        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDTO dto)
        {
            Validar.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(dto.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            if (!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localização de destino não existe");
            }

            if (patrimonio.LocalizacaoId == dto.LocalizacaoID)
            {
                throw new DomainException("O patrimônio já está nessa localização");
            }

            if (_repository.ExisteSolicitacaoPendente(dto.PatrimonioID))
            {
                throw new DomainException("Já existe uma solicitação pendente para esse patrimônio");
            }

            if (usuario.TipoUsuario.Nome == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoId);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("O responsável só pode solicitar transferência do patrimônio do ambiente ao qual está vinculado");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                throw new DomainException("Status de transferência pendente não encontrado");
            }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataSolicitacao = DateTime.Now,
                Justificativa = dto.Justificativa,
                StatusTransferenciaId = statusPendente.StatusTransferenciaId,
                UsuarioSolicitacaoId = usuarioId,
                UsuarioAprovacaoId = null,
                PatrimonioId = dto.PatrimonioID,
                LocalizacaoId = dto.LocalizacaoID
            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDTO dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);
            if (usuario == null)
                throw new DomainException("Usuário não encontrado");

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);
            if (solicitacao == null)
                throw new DomainException("Solicitação de transferência não encontrada.");

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(solicitacao.PatrimonioId);
            if (patrimonio == null)
                throw new DomainException("Patrimônio não encontrado");

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");
            if (statusPendente == null)
                throw new DomainException("Status de transferência pendente não encontrado");

            if (solicitacao.StatusTransferenciaId != statusPendente.StatusTransferenciaId)
                throw new DomainException("Essa solicitação já foi respondida");

            if (usuario.TipoUsuario.Nome == "Responsavel")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoId);

                if (!usuarioResponsavel)
                    throw new DomainException("Somente o responsável do ambiente pode responder a solicitação de transferência");
            }

            StatusTransferencia statusResposta = _repository.BuscarStatusTransferenciaPorNome(dto.Aprovado ? "Aprovada" : "Recusada");

            if (statusResposta == null)
                throw new DomainException("Status de transferência para resposta não encontrado");

            solicitacao.StatusTransferenciaId = statusResposta.StatusTransferenciaId;
            solicitacao.UsuarioAprovacaoId = usuarioId;
            solicitacao.DataResposta = DateTime.Now;

            _repository.Atualizar(solicitacao);

            if (dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");
                if (statusTransferido == null)
                    throw new DomainException("Status de patrimônio transferido não encontrado");

                TipoAlteracao tipoALteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");
                if (tipoALteracao == null)
                    throw new DomainException("Tipo de alteração para transferência não encontrado");

                patrimonio.LocalizacaoId = solicitacao.LocalizacaoId;
                patrimonio.StatusPatrimonioId = statusTransferido.StatusPatrimonioId;

                _repository.AtualizarPatrimonio(patrimonio);
                
                Log_Patrimonio log = new Log_Patrimonio
                {
                    DataTranferencia = DateTime.Now,
                    TipoAlteracaoId = tipoALteracao.TipoAlteracaoId,
                    PatrimonioId = patrimonio.PatrimonioId,
                    StatusPatrimonioId = statusTransferido.StatusPatrimonioId,
                    UsuarioId = usuarioId,
                    LocalizacaoId = solicitacao.LocalizacaoId
                };

                _repository.AdicionarLog(log);
            }
        }
    }
}
