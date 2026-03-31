using Assets_menagement_system.Domains;

namespace Assets_menagement_system.DTOs.StatusPatrimonioDTO
{
    public class LerStatusPatrimonioDTO
    {
        public Guid StatusPatrimonioId { get; set; }

        public string NomeStatus { get; set; } = null!;

        public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

        public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
    }
}
