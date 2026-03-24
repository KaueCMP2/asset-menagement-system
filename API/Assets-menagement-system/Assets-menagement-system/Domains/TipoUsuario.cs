using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class TipoUsuario
{
    public Guid TipoUsuarioId { get; set; }

    public string Nome { get; set; } = null!;
}
