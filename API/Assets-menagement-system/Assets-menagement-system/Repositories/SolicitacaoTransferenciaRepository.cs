using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;

namespace Assets_menagement_system.Repositories
{
    public class SolicitacaoTransferenciaRepository
    {
        private readonly AssetMenagementDbContext _context;

        public SolicitacaoTransferenciaRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia
                .OrderByDescending(solicitacao => solicitacao.DataSolicitacao)
                .ToList();
        }

        public SolicitacaoTransferencia BuscarPorId(Guid transferenciaId)
        {
            return _context.SolicitacaoTransferencia.Find(transferenciaId);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                return false;
            }

            return _context.SolicitacaoTransferencia.Any(solicitacao =>
                solicitacao.PatrimonioId == patrimonioId &&
                solicitacao.StatusTransferenciaId == statusPendente.StatusTransferenciaId
            );
        }

        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId)
        {
            return _context.Usuario.Any(usuario => usuario.UsuarioId == usuarioId &&
            usuario.Localizacao.Any(localizacao => localizacao.LocalizacaoId == localizacaoId)
            );
        }

        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicitacaoTransferencia);
            _context.SaveChanges();
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(localizacao => localizacao.LocalizacaoId == localizacaoId);
        }

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeTipoAlteracao.ToLower() == nomeTipo.ToLower());
        }
        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            if (solicitacaoTransferencia == null)
                return;

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia.Find(solicitacaoTransferencia.SolicitacaoId);

            if (solicitacaoBanco == null)
                return;

            solicitacaoBanco.DataResposta = solicitacaoTransferencia.DataResposta;
            solicitacaoBanco.StatusTransferenciaId = solicitacaoTransferencia.StatusTransferenciaId;
            solicitacaoBanco.UsuarioAprovacaoId = solicitacaoTransferencia.UsuarioAprovacaoId;

            _context.SaveChanges();
        }

        public void AtualizarPatrimonio(Patrimonio patrimonio)
        {
            if (patrimonio == null)
                return;

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioId);

            if (patrimonioBanco == null)
                return;

            patrimonioBanco.LocalizacaoId = patrimonio.LocalizacaoId;
            patrimonioBanco.StatusPatrimonioId = patrimonio.StatusPatrimonioId;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.Imagem = patrimonio.Imagem;

            _context.SaveChanges();
        }

        public void AdicionarLog(Log_Patrimonio log_Patrimonio)
        {
            _context.Log_Patrimonio.Add(log_Patrimonio);
            _context.SaveChanges();
        }
    }
}
