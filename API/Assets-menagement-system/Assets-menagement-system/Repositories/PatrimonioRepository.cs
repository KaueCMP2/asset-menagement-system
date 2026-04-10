using Assets_menagement_system.Contexts;
using Assets_menagement_system.Domains;
using Assets_menagement_system.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;

namespace Assets_menagement_system.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly AssetMenagementDbContext _context;
        public PatrimonioRepository(AssetMenagementDbContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio.OrderBy(p => p.Denominacao).ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio)
        {
            return _context.Patrimonio.FirstOrDefault(p => p.NumeroSerie.ToString() == numeroPatrimonio);
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(l => l.LocalizacaoId == localizacaoId);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Any(s => s.StatusPatrimonioId == statusPatrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null)
                return;
            Patrimonio patrimonioBanco = _context.Patrimonio.FirstOrDefault(p => p.PatrimonioId == patrimonio.PatrimonioId);
            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.NumeroSerie = patrimonio.NumeroSerie;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.LocalizacaoId = patrimonio.LocalizacaoId;
            patrimonioBanco.StatusPatrimonioId = patrimonio.StatusPatrimonioId;
            _context.Patrimonio.Update(patrimonioBanco);
            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
                return;
            Patrimonio patrimonioBanco = _context.Patrimonio.FirstOrDefault(p => p.PatrimonioId == patrimonio.PatrimonioId);
            patrimonioBanco.StatusPatrimonioId = patrimonio.StatusPatrimonioId;
            _context.Patrimonio.Update(patrimonioBanco);
            _context.SaveChanges();
        }

        public void AdicionarLog(Log_Patrimonio log_patrimonio)
        {
            _context.Log_Patrimonio.Add(log_patrimonio);
            _context.SaveChanges();
        }

        public Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao)
        {
            return _context.Localizacao.FirstOrDefault(l => l.NomeLocal == nomeLocalizacao);
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.NomeStatus == nomeStatus);
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.FirstOrDefault(s => s.StatusPatrimonioId == statusPatrimonioId);
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(t => t.NomeTipoAlteracao == nomeTipo);
        }
    }
}