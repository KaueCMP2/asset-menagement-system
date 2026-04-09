using System;
using System.Collections.Generic;
using Assets_menagement_system.Domains;
using Microsoft.EntityFrameworkCore;

namespace Assets_menagement_system.Contexts;

public partial class AssetMenagementDbContext : DbContext
{
    public AssetMenagementDbContext()
    {
    }

    public AssetMenagementDbContext(DbContextOptions<AssetMenagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Area { get; set; }

    public virtual DbSet<Bairro> Bairro { get; set; }

    public virtual DbSet<Cargo> Cargo { get; set; }

    public virtual DbSet<Cidade> Cidade { get; set; }

    public virtual DbSet<Endereco> Endereco { get; set; }

    public virtual DbSet<Localizacao> Localizacao { get; set; }

    public virtual DbSet<Log_Patrimonio> Log_Patrimonio { get; set; }

    public virtual DbSet<Patrimonio> Patrimonio { get; set; }

    public virtual DbSet<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; }

    public virtual DbSet<StatusPatrimonio> StatusPatrimonio { get; set; }

    public virtual DbSet<StatusTransferencia> StatusTransferencia { get; set; }

    public virtual DbSet<TipoAlteracao> TipoAlteracao { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=AssetMenagementDb; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__Area__70B82048CA51C1F4");

            entity.HasIndex(e => e.NomeArea, "UQ__Area__9A779760CC602097").IsUnique();

            entity.Property(e => e.AreaId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeArea)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Bairro>(entity =>
        {
            entity.HasKey(e => e.BairroId).HasName("PK__Bairro__4A0937C396E06257");

            entity.Property(e => e.BairroId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeBairro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cidade).WithMany(p => p.Bairro)
                .HasForeignKey(d => d.CidadeId)
                .HasConstraintName("FK_Bairro_bairroCidade_cidadeId");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK__Cargo__B4E665CDCE34061F");

            entity.HasIndex(e => e.NomeCargo, "UQ__Cargo__4D9FD7DE26FEC47A").IsUnique();

            entity.Property(e => e.CargoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.CidadeId).HasName("PK__Cidade__B6800939B474BF64");

            entity.Property(e => e.CidadeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeCidade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeEstado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.EnderecoId).HasName("PK__Endereco__B9D946CF2F68C314");

            entity.Property(e => e.EnderecoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CEP)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Logradouro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Bairro).WithMany(p => p.Endereco)
                .HasForeignKey(d => d.BairroId)
                .HasConstraintName("FK_Endereco_EnderecoBairro_BairroId");
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.LocalizacaoId).HasName("PK__Localiza__83ABDF2A54829FA3");

            entity.ToTable(tb => tb.HasTrigger("trg_Local_SoftDelete"));

            entity.Property(e => e.LocalizacaoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DescricaoSAP)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomeLocal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusLocalizacao).HasDefaultValue(true);

            entity.HasOne(d => d.Area).WithMany(p => p.Localizacao).HasForeignKey(d => d.AreaId);
        });

        modelBuilder.Entity<Log_Patrimonio>(entity =>
        {
            entity.HasKey(e => e.LogPatrimonioId).HasName("PK__Log_Patr__E716D10BCE4E7E71");

            entity.Property(e => e.LogPatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataTranferencia).HasPrecision(0);

            entity.HasOne(d => d.Localizacao).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_LocalizacaoId");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.PatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_PatrimonioId");

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_StatusPatimonio_StatusPatrimonioId");

            entity.HasOne(d => d.TipoAlteracao).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.TipoAlteracaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_TipoAlteracao_TipoAlteracaoId");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Log_Patrimonio)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_UsuarioId");
        });

        modelBuilder.Entity<Patrimonio>(entity =>
        {
            entity.HasKey(e => e.PatrimonioId).HasName("PK__Patrimon__C5A60BFE7F85CCF8");

            entity.ToTable(tb => tb.HasTrigger("trg_Patrimonio_SoftDelete"));

            entity.Property(e => e.PatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Denominacao).IsUnicode(false);
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Localizacao).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.StatusPatrimonio).WithMany(p => p.Patrimonio)
                .HasForeignKey(d => d.StatusPatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patrimonio_StatusPatrimonioId");
        });

        modelBuilder.Entity<SolicitacaoTransferencia>(entity =>
        {
            entity.HasKey(e => e.SolicitacaoId).HasName("PK__Solicita__6E38856809DC659E");

            entity.Property(e => e.SolicitacaoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataResposta).HasPrecision(0);
            entity.Property(e => e.DataSolicitacao)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Localizacao).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_LocalizacaoId");

            entity.HasOne(d => d.Patrimonio).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.PatrimonioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_PatrimonioId");

            entity.HasOne(d => d.StatusTransferencia).WithMany(p => p.SolicitacaoTransferencia)
                .HasForeignKey(d => d.StatusTransferenciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_StatusTransferenciaId");

            entity.HasOne(d => d.UsuarioAprovacao).WithMany(p => p.SolicitacaoTransferenciaUsuarioAprovacao)
                .HasForeignKey(d => d.UsuarioAprovacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_UsuarioAprovacaoId");

            entity.HasOne(d => d.UsuarioSolicitacao).WithMany(p => p.SolicitacaoTransferenciaUsuarioSolicitacao)
                .HasForeignKey(d => d.UsuarioSolicitacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitacaoTranferencia_UsuarioSolicitacaoId");
        });

        modelBuilder.Entity<StatusPatrimonio>(entity =>
        {
            entity.HasKey(e => e.StatusPatrimonioId).HasName("PK__StatusPa__B3F3362939A1C553");

            entity.HasIndex(e => e.NomeStatus, "UQ__StatusPa__C5C60F1A4AC5F165").IsUnique();

            entity.Property(e => e.StatusPatrimonioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusTransferencia>(entity =>
        {
            entity.HasKey(e => e.StatusTransferenciaId).HasName("PK__StatusTr__7AA82899EC157D9B");

            entity.HasIndex(e => e.NomeStatus, "UQ__StatusTr__C5C60F1A4F579BEB").IsUnique();

            entity.Property(e => e.StatusTransferenciaId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoAlteracao>(entity =>
        {
            entity.HasKey(e => e.TipoAlteracaoId).HasName("PK__TipoAlte__9BEF4F6D8730B2E2");

            entity.HasIndex(e => e.NomeTipoAlteracao, "UQ__TipoAlte__02BC660A2993F049").IsUnique();

            entity.Property(e => e.TipoAlteracaoId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.NomeTipoAlteracao).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioId).HasName("PK__TipoUsua__7F22C722D9B0D06D");

            entity.Property(e => e.TipoUsuarioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B873C679C9");

            entity.ToTable(tb => tb.HasTrigger("trg_Usuario_SoftDelete"));

            entity.HasIndex(e => e.RG, "UQ__Usuario__321537C8700AE784").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053496411334").IsUnique();

            entity.HasIndex(e => e.CPF, "UQ__Usuario__C1F89731EEE7FBDD").IsUnique();

            entity.HasIndex(e => e.NIF, "UQ__Usuario__C7DEC330FFF45FD9").IsUnique();

            entity.Property(e => e.UsuarioId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.CarteiraDeTrabalho)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NIF)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.NomeUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrimeiroAcesso).HasDefaultValue(true);
            entity.Property(e => e.RG)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);

            entity.HasOne(d => d.Cargo).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Endereco).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.EnderecoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_UsuaroEndereco_EnderecoId");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
