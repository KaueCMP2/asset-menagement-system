using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Localizacao
{
    public Guid LocalizacaoId { get; set; }

    public string? NomeLocal { get; set; }

    public int? LocalSAP { get; set; }

    public string? DescricaoSAP { get; set; }

    public bool? StatusLocalizacao { get; set; }

    public Guid? AreaId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();

    public virtual ICollection<SolicitacaoTranferencia> SolicitacaoTranferencia { get; set; } = new List<SolicitacaoTranferencia>();
}
