using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Area
{
    public Guid AreaId { get; set; }

    public string NomeArea { get; set; } = null!;

    public virtual ICollection<Localizacao> Localizacao { get; set; } = new List<Localizacao>();
}
