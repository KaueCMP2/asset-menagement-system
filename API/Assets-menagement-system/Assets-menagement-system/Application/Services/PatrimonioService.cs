using Assets_menagement_system.Application.Mapeamento;
using Assets_menagement_system.Application.Regras;
using Assets_menagement_system.Domains;
using Assets_menagement_system.DTOs.PatrimonioDTO;
using Assets_menagement_system.DTOs.StatusPatrimonioDTO;
using Assets_menagement_system.Exceptions;
using Assets_menagement_system.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

            List<LerPatrimonioDTO> patrimoniosDto = patrimonios.Select(patrimonio => new LerPatrimonioDTO
            {
                PatrimonioId = patrimonio.PatrimonioId,
                Denominacao = patrimonio.Denominacao,
                NumeroSerie = patrimonio.NumeroSerie,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoId = patrimonio.LocalizacaoId,
                StatusPatrimonioId = patrimonio.StatusPatrimonioId
            }).ToList();

            return patrimoniosDto;
        }

        public LerPatrimonioDTO BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado");
            }

            LerPatrimonioDTO patrimonioDto = new LerPatrimonioDTO
            {
                PatrimonioId = patrimonio.PatrimonioId,
                Denominacao = patrimonio.Denominacao,
                NumeroSerie = patrimonio.NumeroSerie,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoId = patrimonio.LocalizacaoId,
                StatusPatrimonioId = patrimonio.StatusPatrimonioId
            };

            return patrimonioDto;
        }

        public void Adicionar(IFormFile arquivoCsv, Guid usuarioId)
        {
            if (arquivoCsv == null || arquivoCsv.Length == 0)
            {
                throw new DomainException("Arquivo CSV é obrigatório.");
            }

            Localizacao localizacaoSemLocal = _repository.BuscarLocalizacaoPorNome("Sem local");

            if (localizacaoSemLocal == null)
            {
                throw new DomainException("Localização 'Sem local' não cadastrada.");
            }

            StatusPatrimonio statusAtivo = _repository.BuscarStatusPatrimonioPorNome("Ativo");

            if (statusAtivo == null)
            {
                throw new DomainException("Status 'Ativo' não cadastrado.");
            }

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Atualização de dados");

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de alteração 'Atualização de dados' não cadastrado.");
            }

            List<ImportarPatrimonioCsvDTO> registros;

            // Abre o arquivo enviado (IFormFile)
            using (var stream = arquivoCsv.OpenReadStream())
            // Lê o arquivo como texto
            using (var reader = new StreamReader(stream))

            // Cria leitor de CSV com configurações personalizadas
            // CultureInfo define como números, datas e textos são interpretados
            // InvariantCulture -> padrão universal
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Define que o separador é ponto e vírgula
                Delimiter = ";",

                // Ignora erros caso o cabeçalho não bata 100%
                // Não trava a aplicação por conta de formatação, vamos tratar os erros depois.
                HeaderValidated = null,

                // Ignora o erro se faltar algum campo
                MissingFieldFound = null,

                // Ignora dados "quebrados" no CSV
                BadDataFound = null, // EX. abriu aspas e não fechou

                // Remove espaços extras automaticamente
                TrimOptions = TrimOptions.Trim
            }))
            {
                // Registra o mapa que criamos (Tradução CSV -> DTO)
                csv.Context.RegisterClassMap<ImportarPatrimonioCsvMap>();

                // Pega todas as linhas do CSV e converte para uma lista de DTO
                registros = csv.GetRecords<ImportarPatrimonioCsvDTO>().ToList();
            }

            var erros = new List<string>();

            foreach (var item in registros)
            {
                // se não tem número de patrimônio, ignora o registro
                if (string.IsNullOrWhiteSpace(item.NumeroSerie))
                {
                    // ignora e vai pro próximo
                    continue;
                }

                // Remove espaços extras do número
                string numeroPatrimonio = item.NumeroSerie.Trim();

                if (string.IsNullOrWhiteSpace(item.Denominacao))
                {
                    erros.Add($"Patrimônio {numeroPatrimonio} sem denominação.");
                    continue; // não cadastra, mas segue no loop
                }

                string denominacao = item.Denominacao.Trim();

                DateTime? dataIncorporacao = null;

                // usa o formato brasileiro só pra ler
                // Depois pega o DateTime e formata
                if (!string.IsNullOrWhiteSpace(item.DataIncorporacao))
                {
                    if (DateTime.TryParse(item.DataIncorporacao, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dataConvertida))
                    {
                        dataIncorporacao = dataConvertida;
                    }
                }

                decimal? valorAquisicao = null;

                if (!string.IsNullOrWhiteSpace(item.Valor))
                {
                    // Remove separador de milhar e ajusta decimal
                    string valorTexto = item.Valor.Replace(".", "").Replace(",", ".");

                    // TryParse - converte string -> decimal
                    // NumberStyles.Any -> define quais formatos de números são permitidos - any aceita qualquer número, mesmo com sinal, com espaço, etc.
                    // out decimal valorConvertido -> se der certo: cria a variavel com o valor já convertido

                    if (decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorConvertido))
                    {
                        valorAquisicao = valorConvertido;
                    }

                    Validar.ValidarNumeroPatrimonio(numeroPatrimonio);
                    Validar.ValidarNome(denominacao);

                    var patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio) ;

                    if (patrimonioExistente == null)
                    {
                        continue;
                    }

                    Patrimonio patrimonio = new Patrimonio
                    {
                        Denominacao = denominacao,
                        NumeroSerie = numeroPatrimonio,
                        Valor = (decimal)valorAquisicao,
                        Imagem = null,
                        LocalizacaoId = localizacaoSemLocal.LocalizacaoId,
                        StatusPatrimonioId = statusAtivo.StatusPatrimonioId
                    };

                    _repository.Adicionar(patrimonio);

                    Log_Patrimonio log = new Log_Patrimonio
                    {
                        DataTranferencia= dataIncorporacao ?? DateTime.Now,
                        TipoAlteracaoId = tipoAlteracao.TipoAlteracaoId,
                        StatusPatrimonioId = patrimonio.StatusPatrimonioId,
                        PatrimonioId = patrimonio.PatrimonioId,
                        UsuarioId = usuarioId,
                        LocalizacaoId = patrimonio.LocalizacaoId
                    };

                    _repository.AdicionarLog(log);
                }
            }
        }

        public void AtualizarStatus(Guid patrimonioId, AtualizarStatusPatrimonioDTO dto)
        {
            Patrimonio patrimonioBanco = _repository.BuscarPorId(patrimonioId);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioId))
            {
                throw new DomainException("Status de patrimônio informado não existe.");
            }

            patrimonioBanco.StatusPatrimonioId = dto.StatusPatrimonioId;

            _repository.AtualizarStatus(patrimonioBanco);
        }
    }
}
