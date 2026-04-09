using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Patrimonio
{
    public Guid PatrimonioId { get; set; }

    public string? Denominacao { get; set; }

    public int NumeroSerie { get; set; }

    public decimal Valor { get; set; }

    public string? Imagem { get; set; }

    public Guid LocalizacaoId { get; set; }

    public Guid StatusPatrimonioId { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;
}
