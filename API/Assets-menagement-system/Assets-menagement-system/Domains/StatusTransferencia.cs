using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class StatusTransferencia
{
    public Guid StatusTransferenciaId { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();
}
