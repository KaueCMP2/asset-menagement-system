using System;
using System.Collections.Generic;

namespace Assets_menagement_system.Domains;

public partial class Usuario
{
    public Guid UsuarioId { get; set; }

    public string NIF { get; set; } = null!;

    public string NomeUsuario { get; set; } = null!;

    public string RG { get; set; } = null!;

    public string CPF { get; set; } = null!;

    public string? CarteiraDeTrabalho { get; set; }

    public byte[]? Senha { get; set; }

    public string Email { get; set; } = null!;

    public bool? StatusUsuario { get; set; }

    public bool? PrimeiroAcesso { get; set; }

    public Guid EnderecoId { get; set; }

    public Guid TipoUsuarioId { get; set; }

    public Guid CargoId { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Endereco Endereco { get; set; } = null!;

    public virtual ICollection<Log_Patrimonio> Log_Patrimonio { get; set; } = new List<Log_Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioAprovacao { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioSolicitacao { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;
}
