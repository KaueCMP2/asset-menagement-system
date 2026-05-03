using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Endereco
{
    public Guid EnderecoId { get; set; }

    public string Logradouro { get; set; } = null!;

    public int Numero { get; set; }

    public string Complemento { get; set; } = null!;

    public string CEP { get; set; } = null!;

    public Guid BairroId { get; set; }

    public virtual Bairro Bairro { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
