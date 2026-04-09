using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class SolicitacaoTransferencia
{
    public Guid SolicitacaoId { get; set; }

    public DateTime? DataSolicitacao { get; set; }

    public DateTime DataResposta { get; set; }

    public string Justificativa { get; set; } = null!;

    public Guid StatusTransferenciaId { get; set; }

    public Guid UsuarioSolicitacaoId { get; set; }

    public Guid? UsuarioAprovacaoId { get; set; }

    public Guid PatrimonioId { get; set; }

    public Guid LocalizacaoId { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual StatusTransferencia StatusTransferencia { get; set; } = null!;

    public virtual Usuario UsuarioAprovacao { get; set; } = null!;

    public virtual Usuario UsuarioSolicitacao { get; set; } = null!;
}
