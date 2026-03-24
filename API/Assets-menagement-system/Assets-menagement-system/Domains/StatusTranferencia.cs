using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class StatusTranferencia
{
    public Guid StatusTransferenciaId { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<SolicitacaoTranferencia> SolicitacaoTranferencia { get; set; } = new List<SolicitacaoTranferencia>();
}
