 using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Log_Patrimonio
{
    public Guid LogPatrimonioId { get; set; }

    public DateTime DataTranferencia { get; set; }

    public Guid TipoAlteracaoId { get; set; }

    public Guid StatusPatrimonioId { get; set; }

    public Guid PatrimonioId { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid LocalizacaoId { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;

    public virtual TipoAlteracao TipoAlteracao { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
