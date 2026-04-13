using Assets_menagement_system.DTOs.PatrimonioDTO;
using CsvHelper.Configuration;

namespace Assets_menagement_system.Application.Mapeamento
{
    // ClassMap => é tipo um "Tradutor de colunas", define como ler o csv
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDTO>
    {
        // Definindo os mapeamentosdas colunas do CSV para as propriedades do DTO
        public ImportarPatrimonioCsvMap()
        {
          // Map escolhe a propiedade da DTO, Name define com ele esta escrito na coluna no CSV
            Map(m => m.NumeroSerie).Name("N° invent.");
            Map(m => m.Denominacao).Name("Denominação do imobilizado");
            Map(m => m.DataImcorporacao).Name("Dt. incorp.");
            Map(m => m.Valor).Name("ValAquis.");
        }
    }
}
