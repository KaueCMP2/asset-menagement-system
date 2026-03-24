using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Bairro
{
    public Guid BairroId { get; set; }

    public string NomeBairro { get; set; } = null!;

    public Guid CidadeId { get; set; }

    public virtual Cidade Cidade { get; set; } = null!;

    public virtual ICollection<Endereco> Endereco { get; set; } = new List<Endereco>();
}
