using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class TipoPatrimonio
{
    public Guid TipoPatrimonioId { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
}
