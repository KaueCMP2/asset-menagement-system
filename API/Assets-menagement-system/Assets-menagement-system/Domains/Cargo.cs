using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Cargo
{
    public Guid CargoId { get; set; }

    public string NomeCargo { get; set; } = null!;
}
