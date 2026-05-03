using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class TipoAlteracao
{
    public Guid TipoAlteracaoId { get; set; }

    public string NomeTipoAlteracao { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();
}
