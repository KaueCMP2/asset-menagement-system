using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class StatusPatrimonio
{
    public Guid StatusPatrimonioId { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
}
