using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Cidade
{
    public Guid CidadeId { get; set; }

    public string NomeCidade { get; set; } = null!;

    public string NomeEstado { get; set; } = null!;

    public virtual ICollection<Bairro> Bairro { get; set; } = new List<Bairro>();
}
