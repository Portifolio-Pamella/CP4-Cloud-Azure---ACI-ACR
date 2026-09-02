using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Aegis.Api.Models;

namespace Aegis.Api.Data.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("TB_EMPRESA_AEROESPACIAL");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ID_EMPRESA");

            builder.Property(e => e.Nome)
                .HasColumnName("NOME_EMPRESA")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Cnpj)
                .HasColumnName("CNPJ_ID_EMPRESA")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.PaisOrigem)
                .HasColumnName("PAIS_ORIGEM")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("STATUS_OPERACAO")
                .HasMaxLength(20)
                .IsRequired();

            // Relacionamento 1:N com Satélites
            builder.HasMany(e => e.Satelites)
                .WithOne(s => s.Empresa)
                .HasForeignKey(s => s.EmpresaId);
        }
    }
}