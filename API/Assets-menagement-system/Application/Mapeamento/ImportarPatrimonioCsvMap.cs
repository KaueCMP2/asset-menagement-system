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
            Map(item => item.NumeroSerie).Name("N° invent.");
            Map(item => item.Denominacao).Name("Denominação do imobilizado");
            Map(item=> item.DataIncorporacao).Name("Dt. incorp.");
            Map(item => item.Valor).Name("ValAquis.");
        }
    }
}
